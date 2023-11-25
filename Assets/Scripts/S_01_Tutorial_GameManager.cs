using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_01_Tutorial_GameManager : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> slides;
    [SerializeField] private InputActionReference _inputXAction = default;
    [SerializeField] private InputActionReference _inputScanAimAction = default;

    [SerializeField] private bool allowInput;

    [SerializeField] private int currentSlide;
    [SerializeField] private CanvasGroup _blackScreen;
    [SerializeField] private bool transitionInProgress;
    [SerializeField] private string scenename;
    [SerializeField] private AudioSource _buttonPress;
    

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.alphaCanvas(slides[0], 1, 2f).setDelay(2f).setEaseLinear()
            .setOnComplete((() => { allowInput = true; }));
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputScanAimAction.action.ReadValue<float>() != 0 && allowInput && !transitionInProgress)
        {
            Debug.Log("A PRESSED");
            transitionInProgress = true;
            _buttonPress.Play();
            switch (currentSlide)
            {
                case 1:
                {
                    ShowDroneWithInput();
                }
                    break;
                case 3:
                {
                    GameReady();
                }
                    ;
                    break;
                default:
                {
                    NextSlide();
                }
                    break;
            }
        }
        if(_inputXAction.action.ReadValue<float>() != 0 && allowInput && !transitionInProgress)
        {
            StartGame();
        }
    }

    public void GameReady()
    {
        allowInput = false;
        LeanTween.alphaCanvas(slides[currentSlide], 0, 1f).setEaseLinear().setOnComplete((() => { StartGame(); }));
    }

    public void ShowDroneWithInput()
    {
        LeanTween.alphaCanvas(slides[currentSlide], 0, 1f).setEaseLinear().setOnComplete((() =>
        {
            currentSlide += 1;
            LeanTween.alphaCanvas(_blackScreen, 0, 1f).setEaseLinear().setOnComplete((() =>
            {
                LeanTween.alphaCanvas(_blackScreen, 1, 1f).setDelay(5).setEaseLinear().setOnComplete((() =>
                {
                    LeanTween.alphaCanvas(slides[currentSlide], 1, 1f).setEaseLinear().setOnComplete((() =>
                    {
                        allowInput = true;
                        transitionInProgress = false;
                    }));
                }));
            }));
        }));
    }

    public void NextSlide()
    {
        allowInput = false;
        LeanTween.alphaCanvas(slides[currentSlide], 0, 1f).setEaseLinear().setOnComplete((() =>
        {
            currentSlide += 1;
            LeanTween.alphaCanvas(slides[currentSlide], 1, 1f).setEaseLinear().setOnComplete((() =>
            {
                allowInput = true;
                transitionInProgress = false;
            }));
        }));
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            Debug.Log("sceneName to load: " + scenename);
            SceneManager.LoadScene(scenename);
        }

    }

    private bool gameStarted = false;
}