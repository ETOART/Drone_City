using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.InputSystem;

public class S_00_VideoScreen_GameManager : MonoBehaviour
{

    [SerializeField] private string scenename = "S_02_DroneSelect";
    public bool start = false;
    [SerializeField] private ServerStart serverStart;
    [SerializeField] private SessionController sessionController;

    [SerializeField] private InputActionReference _inputXAction = default;

    [SerializeField] private CanvasGroup _transitScreen;

    private bool oneShotmethod = true;
    private void Start()
    {
        sessionController.isGame = false;
        LeanTween.alphaCanvas(_transitScreen, 0, 1f).setEaseLinear();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || _inputXAction.action.ReadValue<float>() != 0) && oneShotmethod )
        {
            oneShotmethod = false;
            SessionController.gameIsRegister = false;
            sessionController.isGame = true;
            LeanTween.alphaCanvas(_transitScreen, 1, 1f).setDelay(0.1f).setEaseLinear().setOnComplete(LoadScene);
        }
        if (start)
        {
            start = false;
            SessionController.gameIsRegister = true;
            sessionController.isGame = true;
            LeanTween.alphaCanvas(_transitScreen, 1, 1f).setDelay(0.1f).setEaseLinear().setOnComplete(LoadScene);  
        }

    }

    private void LoadScene()
    {
        SceneManager.LoadSceneAsync(scenename);
    }
   

}
