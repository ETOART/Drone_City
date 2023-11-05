using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject HitSphere;

    [SerializeField] private GameObject scanArea;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField] private List<Camera>  _cameras;
    
    
    private void OnDrawGizmos()
    {

        Ray ray;
        ray =_camera.ScreenPointToRay(Input.mousePosition);
       // Debug.Log(Input.mousePosition);
        
        //ray =  _camera.ViewportPointToRay(scanArea.transform.position);
        


        int layerMask = 1 << 10;
        RaycastHit hit;
            
        if (Physics.Raycast(ray, out hit, 1000000,layerMask))
        {
            Debug.DrawRay (ray.origin, ray.direction * 50000000, Color.white);
            //Gizmos.DrawSphere(hit.point, 10);
            Vector3 pos = hit.point;
           
            HitSphere.transform.position = pos;
        }
               
        else
        {
            Debug.DrawRay (ray.origin, ray.direction * 50000000, Color.red);
           // HitSphere.transform.position = Vector3.zero;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        

            
            
           // Debug.DrawRay (ray.origin, ray.direction * 50000000, Color.red);
           // _lineRenderer.SetPosition(0,_camera.transform.position);        
           // _lineRenderer.SetPosition(1,_camera.transform.position);        
       
    }
}
