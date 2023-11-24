using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WaitToLoadStartScene : MonoBehaviour
{
    [SerializeField] private CanvasGroup _transitScreen;
    [SerializeField] private float timeToWait = 20f;
    [SerializeField] private bool startTransitionScreen = true;
    bool start = false;
    void Start()
    {
        if (startTransitionScreen)
        LeanTween.alphaCanvas(_transitScreen, 0, 1f).setEaseLinear();

        StartCoroutine(WaitToLoadScene());
    }
    private void Update()
    {
        if (start)
        {
            start = false;
            LeanTween.alphaCanvas(_transitScreen, 1, 1f).setDelay(0.1f).setEaseLinear().setOnComplete(RestartGame); 
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadSceneAsync("S_00_VideoScreen");
    }
    
    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(timeToWait);
        start = true;

    }
}
