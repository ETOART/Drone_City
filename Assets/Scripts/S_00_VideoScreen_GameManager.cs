using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class S_00_VideoScreen_GameManager : MonoBehaviour
{

    [SerializeField] private string scenename = "S_02_DroneSelect";
    private bool start = false;
    [SerializeField] private ServerStart serverStart;
    [SerializeField] private SessionController sessionController;
    private void Start()
    {
        sessionController.isGame = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
       // if (start)
       // {
       //     StartGame();
       //     start = false;
       // }

    }

    public void StartGame()
    {
        if (!start)
        {
            start = true;
            Debug.Log("sceneName to load: " + scenename);
            sessionController.isGame = true;
            SceneManager.LoadSceneAsync(scenename);
        }
    }
   

}
