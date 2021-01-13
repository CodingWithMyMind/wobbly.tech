using UnityEngine.UI;
using UnityEngine;


    public class TwitterManager : MonoBehaviour
    {
        GameObject homeButton;

         Text locationText;

        Spawner spawner;

        

        private static TwitterManager _instance;
        public static TwitterManager Instance
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

            homeButton = GameObject.Find("TwitterHomeButton");
            locationText = GameObject.FindWithTag("locationText").GetComponent<Text>();
            spawner = GameObject.Find("TweetSpawner").GetComponent<Spawner>();

            EnterMenu();
            
        }


    public void EnterMenu()
    {
        
        locationText.text = null;

        
        spawner.StopCoroutine("SpawnTweetSetup");
        spawner.StartCoroutine("PurgeTweets");
        spawner.canTweetSpawn = false;

        spawner.StartCoroutine("SpawnMenuItems");

        homeButton.SetActive(false);
    }

    public void EnterPlaying()
    {
        spawner.canTweetSpawn = true;
        spawner.StopCoroutine("SpawnMenuItems");
        spawner.StartCoroutine("PurgeMenuItems");


        homeButton.SetActive(true);
    }
}
