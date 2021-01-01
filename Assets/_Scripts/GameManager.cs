using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum State { Menu, Main };

    public GameObject MenuUI, MainUI;

    //public GameObject MapNavUI, ARModeNavUI, LocationsNavUI, GalleryNavUI, InfoNavUI;



    public State state;


    private static GameManager _instance;

    public static GameManager Instance
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

    private void Awake()
    {

        //#if UNITY_STANDALONE_WIN
        //        windowsBuild = true;
        //#endif

        //#if UNITY_IOS
        //      IOSBuild = true;
        //#endif

        Instance = this;
    }

    void Start()
    {
        state = State.Main;
    }

    void Update()
    {
        switch (state)
        {
            case State.Menu:
                {
                    break;
                }

            case State.Main:
                {
                    break;
                }
        }
    }

    public void EnterMenu()
    {


        //TurnOnUI(MenuUI,MenuNavUI);

        state = State.Menu;
    }




    // do once here

    public void EnterMain()
    {
        //SceneManager.LoadSceneAsync("main", LoadSceneMode.Single);

        StartCoroutine(AsynchronousLoad("Main"));

        state = State.Main;
    }

    IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        //ao.allowSceneActivation = false;

/*        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            {
                Debug.Log("Press a key to start");
                if (Input.anyKey)
                    ao.allowSceneActivation = true;
            }

            yield return null;
        }*/
    }


}
