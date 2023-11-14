using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    #region Time

    public float timeRemaining = 60 * 3;
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
    
    
    
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        instance = this;
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
                GameEnd();
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
    }
}