using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class S_00_VideoScreen_GameManager : MonoBehaviour
{

    [SerializeField] private string scenename = "S_02_DroneSelect";
    public bool start = false;
    [SerializeField] private ServerStart serverStart;
    [SerializeField] private SessionController sessionController;

    [SerializeField] private InputActionReference _inputXAction = default;
    [SerializeField] private InputActionReference _inputAAction = default;

    [SerializeField] private CanvasGroup _transitScreen;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<VideoClip> clips;

    private CanvasGroup playerAlpha;

    private bool waitToChangeClip = false;

    private bool oneShotmethod = true;
    private void Start()
    {
        sessionController.isGame = false;
        LeanTween.alphaCanvas(_transitScreen, 0, 1f).setEaseLinear();
        videoPlayer.loopPointReached += loopPointReached;
        playerAlpha = videoPlayer.gameObject.GetComponent<CanvasGroup>();
    }
    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= loopPointReached;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || _inputAAction.action.ReadValue<float>() != 0) && oneShotmethod )
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


        if (((Input.GetKeyDown(KeyCode.C) || _inputXAction.action.ReadValue<float>() != 0) && oneShotmethod) && !waitToChangeClip)
        {
            StartStatVideo();
        }

    }

    private void LoadScene()
    {
        SceneManager.LoadSceneAsync(scenename);
    }
   
    private void StartStatVideo()
    {
        ShowNextVideo(clips[1]);
        waitToChangeClip = true;
    }

    private void ShowNextVideo(VideoClip clip)
    {
        LeanTween.alphaCanvas(playerAlpha, 0, 1f).setEaseLinear().setOnComplete((() =>
        {
            videoPlayer.clip = clip;
            videoPlayer.Play();
            LeanTween.alphaCanvas(playerAlpha, 1, 1.5f).setDelay(0.5f).setEaseLinear();

        }));


    }
    public void loopPointReached(VideoPlayer source){
        if (waitToChangeClip)
        {
            ShowNextVideo(clips[0]);
            waitToChangeClip = false;
        }
    }
}
