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
    private void Start()
    {
        IDGenerator.isGame = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        if (start)
        {
            StartGame();
            start = false;
        }

    }

    public string StartGame()
    {
        start = true;
        IDGenerator.isGame = true;
        Debug.Log("sceneName to load: " + scenename);
        SceneManager.LoadSceneAsync(scenename);
        string ID = IDGenerator.TakeNewID();
        Debug.Log("Generate new ID: " + ID);
        return ID;

    }
   

}
