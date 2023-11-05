using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimObject : MonoBehaviour
{

    [SerializeField] private Material _simple;
    [SerializeField] private Material _ready;
    [SerializeField] private MeshRenderer scanerPlane;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform, transform.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");

        scanerPlane.material = _ready;
        

    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Hit exit");
        scanerPlane.material = _simple;
    }
}
