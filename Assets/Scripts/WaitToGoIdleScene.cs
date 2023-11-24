using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WaitToGoIdleScene : MonoBehaviour
{
    [SerializeField] private float _timeToWait = 60f;
    private bool start = false;
    private void Update()
    {
        if(start) 
    }
    IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(_timeToWait);
        start = true;

    }
}
