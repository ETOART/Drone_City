using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AimObject : MonoBehaviour
{

    [SerializeField] private Material _simple;
    [SerializeField] private Material _ready;
    [SerializeField] private MeshRenderer scanerPlane;
    [SerializeField] private bool readyToCheck;
    [SerializeField] private GameObject scanObject;
    [SerializeField] private GameObject crossObject;
    [SerializeField] private bool block;
    [SerializeField] private GameObject _player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform, transform.up);
        if (readyToCheck && scanObject!=null && !block)
        {
            if (Input.GetKey(KeyCode.Space))
            {
               
                scanObject.tag = "done";
                block = true;
               GameObject cross =  Instantiate(crossObject, scanObject.transform);
              // cross.GetComponent<LookAtConstraint>().AddSource(_pl);
               
               
               LeanTween.moveY(cross, cross.transform.position.y + 25, 3).setEaseOutCirc().setOnComplete((o =>
               {
                    //Destroy(cross);
                    block = false;
               }));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if(other.CompareTag("done")) return;
        if (other.CompareTag("target"))
        {
            scanerPlane.material = _ready;
            scanObject = other.gameObject;
        
            readyToCheck = true;
        }
        
       

    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Hit exit");
        scanerPlane.material = _simple;
        readyToCheck = false;
        scanObject = null;
    }
}
