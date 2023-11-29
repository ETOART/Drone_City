using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] private CanvasGroup _transitScreen;
    [SerializeField] private bool transitionInProgress;
    [SerializeField] private string scenename;
    [SerializeField] private AudioSource _buttonPress;


    [SerializeField] private AudioMixer audioMixer; // Ссылка на аудио микшер
    [SerializeField] private string dronGroupToMute = "Dron";

    private bool dronShow = false;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.alphaCanvas(slides[0], 1, 2f).setDelay(2f).setEaseLinear()
            .setOnComplete((() => { allowInput = true; }));
        LeanTween.alphaCanvas(_transitScreen, 0, 1f).setEaseLinear();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputScanAimAction.action.ReadValue<float>() != 0 && allowInput && !transitionInProgress)
        {
            Debug.Log("A PRESSED");
            transitionInProgress = true;
            _buttonPress.Play();

            if(dronShow)
            {
                HideDrontWithInput();
                return;
            }
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
        if((Input.GetKeyDown(KeyCode.Space) || _inputXAction.action.ReadValue<float>() != 0) && allowInput && !transitionInProgress)
        {
            LeanTween.alphaCanvas(_transitScreen, 1, 1f).setDelay(0.1f).setEaseLinear().setOnComplete(StartGame); 
        }
    }

    public void GameReady()
    {
        allowInput = false;
        LeanTween.alphaCanvas(slides[currentSlide], 0, 1f).setEaseLinear().setOnComplete((() => { StartGame(); }));
    }

    public void ShowDroneWithInput()
    {
        
        audioMixer.SetFloat(dronGroupToMute, 1f);
        LeanTween.alphaCanvas(slides[currentSlide], 0, 1f).setEaseLinear().setOnComplete((() =>
        {
            dronShow = true;
            allowInput = true;
            transitionInProgress = false;
            
            LeanTween.alphaCanvas(_blackScreen, 0, 1f).setEaseLinear();
        }));
    }

    public void HideDrontWithInput()
    {
        audioMixer.SetFloat(dronGroupToMute, -80f);
        dronShow = false;
        LeanTween.alphaCanvas(_blackScreen, 1, 1f).setEaseLinear().setOnComplete((() =>
        {
            currentSlide += 1;
            LeanTween.alphaCanvas(slides[currentSlide], 1, 1f).setEaseLinear().setOnComplete((() =>
            {

                allowInput = true;
                transitionInProgress = false;
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
            SceneManager.LoadSceneAsync(scenename);
        }

    }

    private bool gameStarted = false;
}