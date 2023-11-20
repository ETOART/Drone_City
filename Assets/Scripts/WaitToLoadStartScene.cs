using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WaitToLoadStartScene : MonoBehaviour
{
    bool start = false;
    void Start()
    {
        StartCoroutine(WaitToLoadScene());
    }
    private void Update()
    {
        if (start)
        {
            start = false;
            SceneManager.LoadSceneAsync("S_00_VideoScreen");
        }
    }


    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(30f);
        start = true;

    }
}
