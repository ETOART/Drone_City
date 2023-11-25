using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CytiBilBord : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private Transform city;
    [SerializeField] private float treshholdCityDist = 50f;
    private float camDistRef;

    private void Start()
    {
        camDistRef = camera.position.z - transform.position.z;
    }

    private void Update()
    {
        float camDist = camera.position.z - transform.position.z;
        float cityDist = city.position.z - transform.position.z;
        //Debug.Log("cityDist:  "+ cityDist);
        if (cityDist > treshholdCityDist) return;
        transform.position -= new Vector3(0, 0, camDistRef - camDist);
    }

}
