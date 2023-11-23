using System.Collections;
using System.Collections.Generic;
using DroneController;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    #region Time

    public float timeRemaining =  3;//60 *
    [SerializeField] private float endingAlarmTime;
    [SerializeField] private bool endingAlarmDone;
    
    
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;

    #endregion

    #region Score

    [SerializeField] private TextMeshProUGUI _scoreUI;
    [SerializeField] private int score;
    #endregion

    public static GameManager instance;

    [SerializeField] private Image loadingImage;
    [SerializeField] private float loadingProgress;

    [SerializeField] private TextMeshProUGUI scoreToAdd;
    [SerializeField] private  TextMeshProUGUI textToAdd;
    [SerializeField] private CanvasGroup targetScanAreaUI;
    [SerializeField] private CanvasGroup _UIgroup;
    


    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject drone;
    [SerializeField] private Transform droneMoveTarget;
    
    [SerializeField] private CanvasGroup _blackScreen;


    [SerializeField] private CanvasGroup _mainUI;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameObject _droneContainer;

    [SerializeField] private List<GameObject> drones;

    [SerializeField] private HttpRequest httpRequest;

    [SerializeField] private AudioSource _timeUpSound;
    [SerializeField] private AudioSource _timeEndingSound;






    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        instance = this;
        int drone = PlayerPrefs.GetInt("DroneSelected");
        GameObject droneToCreate = drones[drone];
        Instantiate(droneToCreate, _droneContainer.transform);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                DisplayProgress();
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                _timeUpSound.Play();
                GameEnd();
            }
            
            if (timeRemaining < endingAlarmTime && !endingAlarmDone)
            {
                endingAlarmDone = true;
                _timeEndingSound.Play();
            }
            
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = "" + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void DisplayProgress()
    {
        loadingImage.fillAmount -= 1.0f / 180 * Time.deltaTime;
    }

    public void AddScore()
    {
        score += 1;
        _scoreUI.text = "" + score;
        
    }


    public void GameEnd()
    {
        
        Debug.Log("Game over");
        Debug.Log("Score : " + score);


        httpRequest.SendScore(score);

        Camera.GetComponent<CameraMovement>().enabled = false;
        LeanTween.alphaCanvas(_mainUI, 0, 2f).setDelay(2f).setEaseLinear();
        _inputManager.OnDisable();
        
        
        LeanTween.move(drone, droneMoveTarget, 5f).setOnComplete((() =>
        {
            
            LeanTween.alphaCanvas(_blackScreen, 1, 2f).setDelay(2f).setEaseLinear().setOnComplete(StartScoreLevel);

            
        }));

    }

    public void StartScoreLevel()
    {
        SceneManager.LoadSceneAsync(4 , LoadSceneMode.Additive);
    }

    public void ShowScanTargetData(GameObject target)
    {
        ScanTarget scanTarget = target.GetComponent<ScanTarget>();
        if (scanTarget != null)
        {
            scoreToAdd.text = "+"+ scanTarget.scoreToAdd;
            scoreToAdd.text = scanTarget.name;  
        }

        LeanTween.alphaCanvas(targetScanAreaUI, 1, 1f).setEaseLinear();


        LeanTween.alphaCanvas(targetScanAreaUI, 0, 1f).setEaseLinear().setDelay(5);


    }
}