using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_Village : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private Transform city;
    [SerializeField] private float tresholdCityDist = 1800f;
    [SerializeField] private AudioSource enviromentSound;
    [SerializeField] private AudioClip clip;

    private bool state = false;

    private void Update()
    {
        
        float camDist = camera.position.z - city.position.z;
        Debug.Log(camDist);
        if (camDist < tresholdCityDist && !state)
        {
            state = true;
            enviromentSound.clip = clip;
            enviromentSound.Play();
        }
        
    }
}
