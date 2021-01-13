using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    public float waitTime = 0.1f;

    public Material[] materials;
    public GameObject tweetPrefab;

    public GameObject[] countryMenuItems;

    public bool canMenuSpawn;
    public bool canTweetSpawn = true;

    public  List<GameObject> tweetItemsInstances = new List<GameObject>();
    public List<GameObject> MenuItemsInstances = new List<GameObject>();

    private static Spawner _instance;
    public static Spawner Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            if (_instance == null)
                _instance = value;
        }
    }

    void Start()
    {
        Instance = this;
    }


    bool CollisionCheck(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        if (hitColliders.Length == 0) return true;
        return false;
    }


    public IEnumerator SpawnMenuItems()
    {
        for (int i = 0; i < countryMenuItems.Length; i++)
        {
            if (canMenuSpawn)
            {
                bool spawned = false;
                while (!spawned)
                {
                    // New position with random x and y
                    Vector3 pos = RandomPos();
            

                    if (CollisionCheck(pos, 0.7f) && canMenuSpawn)
                    {
                        GameObject instance = Instantiate(countryMenuItems[i], pos, Quaternion.identity);
                       
                        instance.transform.localScale = new Vector3(0, 0, 0);
                        MenuItemsInstances.Add(instance);
                        yield return new WaitForSeconds(waitTime);
                        if (instance != null)
                        {
                            instance.transform.DOScale(instance.GetComponent<Attractor>().finalSize, 1f);
                        }
                        spawned = true;
                    }
                    else
                    {
                        pos = RandomPos();
                    }
                }
            }
        }
    }

    public IEnumerator PurgeMenuItems()
    {
        if (MenuItemsInstances != null)
        {
            for (int i = 0; i < MenuItemsInstances.Count; i++)
            {
                MenuItemsInstances[i].transform.DOScale(0, waitTime / 5f);
                Destroy(MenuItemsInstances[i], waitTime / 5);

                yield return new WaitForSeconds(waitTime / 5);
            }
            MenuItemsInstances.RemoveAll((o) => o == null);
         }
    }

    public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

    public void SpawnTweetWrapper(SimpleJSON.JSONNode jsonResponse,float maxSize,float minSize,float maxVol,float minVol)
    {
        StartCoroutine(SpawnTweetSetup(jsonResponse,maxSize,minSize,maxVol,minVol));
    }

    public IEnumerator SpawnTweetSetup(SimpleJSON.JSONNode jsonResponse,float maxSize, float minSize,float maxVol, float minVol)
    {
        int tweetColour = Random.Range(0, materials.Length);

        for (int i = 0; i < API.Instance.amountOfResponses; i++)
        {
            if (canTweetSpawn)
            {
                string url = jsonResponse[0]["trends"][i]["url"].Value;
                string responseNameText = jsonResponse[0]["trends"][i]["name"].Value;
                int hotness = jsonResponse[0]["trends"][i]["tweet_volume"].AsInt;

                float hotnessRemapped;

                if (hotness == 0)  hotnessRemapped = 1.5f; 
                else               hotnessRemapped = Remap(hotness, minVol, maxVol, minSize, maxSize);

                Vector3 remappedSizeVector = new Vector3(hotnessRemapped, hotnessRemapped, hotnessRemapped);

                Vector3 pos = RandomPos();
                
                

                bool spawned = false;

                while (!spawned)
                {
                    if (CollisionCheck(pos, 0.1f))
                    {
                        InstantiateTweet(tweetPrefab, pos, remappedSizeVector, responseNameText,url,tweetColour);
                        spawned = true;
                    }
                    else
                    {
                        pos = RandomPos();
                    }
                }
                yield return new WaitForSeconds(waitTime);
            }
        }
        canTweetSpawn = true;
    }
    
    public Vector3 RandomPos()
    {

        Vector3 pos = new Vector3(Random.Range(-24f + this.transform.position.x, 24f+this.transform.position.x), Random.Range(-24f + this.transform.position.y, 24f+ this.transform.position.y),this.transform.position.z);
        return pos;
    }

    public void InstantiateTweet(GameObject tweetPrefab, Vector3 pos, Vector3 remapped, string responseNameText,string url,int colour)
    {


        GameObject instance = Instantiate(tweetPrefab, pos,Quaternion.Euler(0,0,0)); ;

        // Apply Material
        instance.GetComponentInChildren<Renderer>().material = materials[colour];

        instance.GetComponent<Attractor>().finalSize = remapped;
        instance.GetComponent<Attractor>().UpdateMass();
        // Set size

        instance.transform.localScale = new Vector3(0, 0, 0);
        instance.transform.DOScale(instance.GetComponent<Attractor>().finalSize.x, 1f);

        // Setting tweet Names
        instance.GetComponentInChildren<Text>().text = responseNameText;

        instance.name = responseNameText;

        instance.GetComponentInChildren<Hyperlink>().url = url;

        tweetItemsInstances.Add(instance);

    }

    public IEnumerator PurgeTweets()
    {
        Debug.Log("purging tweets");
        if (tweetItemsInstances != null)
        {
            for (int i = 0; i < tweetItemsInstances.Count; i++)
            {
                tweetItemsInstances[i].transform.DOScale(0, waitTime / 5f);
                Destroy(tweetItemsInstances[i]);

                yield return new WaitForSeconds(waitTime/5);

            }
            tweetItemsInstances.RemoveAll((o) => o == null);
        }
    }
}
