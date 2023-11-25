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

    [SerializeField] private CanvasGroup _transitScreen;
    private void Start()
    {
        sessionController.isGame = false;
        LeanTween.alphaCanvas(_transitScreen, 0, 1f).setEaseLinear();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            start = false;
            LeanTween.alphaCanvas(_transitScreen, 1, 1f).setDelay(0.1f).setEaseLinear().setOnComplete(StartGame);
        }
        if (start)
        {
            start = false;
            LeanTween.alphaCanvas(_transitScreen, 1, 1f).setDelay(0.1f).setEaseLinear().setOnComplete(StartGame);  
        }

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
