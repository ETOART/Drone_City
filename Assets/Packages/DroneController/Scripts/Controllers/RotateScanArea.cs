using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScanArea : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject HitSphere;
    [SerializeField] private Camera main_camera;
    [SerializeField] private GameObject _RCenter;
    
    
    // Start is called before the first frame update
    void Start()
    {
        main_camera = _camera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKey(KeyCode.LeftArrow))
       // {
       //     transform.RotateAround(_camera.transform.position, Vector3.up, -40 * Time.deltaTime);
       //   
       // }
//
       // if (Input.GetKey(KeyCode.RightArrow))
       // {
       //     transform.RotateAround(_camera.transform.position, Vector3.up, 40 * Time.deltaTime);
       // }
       // 
       // 
       // if (Input.GetKey(KeyCode.UpArrow))  
       // {
       //     transform.RotateAround(_camera.transform.position, Vector3.right, -20 * Time.deltaTime);
       //     
       // }
       // 
       // if (Input.GetKey(KeyCode.DownArrow))
       // {
       //     transform.RotateAround(_camera.transform.position, Vector3.right, 20 * Time.deltaTime);
       // }
    }



    public void moveScanAreaUPDOWN(float value)
    {
        if (value != 0)
        {
            Vector3 viewPos = main_camera.WorldToViewportPoint(transform.position);
            Debug.Log("X:" + viewPos.x);
            
            
            if (viewPos.x >=0 && value < 0)
            {
                
                transform.RotateAround(_RCenter.transform.position, Vector3.up, 40 * value * Time.deltaTime);
                
            }
            else
            {
                Debug.Log("Left Border of screen has reached");
            }
            
            
            
           if (viewPos.x <= 1 && value > 0)
           {
               
               transform.RotateAround(_RCenter.transform.position, Vector3.up, 40 * value * Time.deltaTime);
           }
           else
           {
               Debug.Log("Right Border of screen has reached");
           }
            
        }
       
        
            
    }
    
    public void moveScanAreaLEFTRIGHT(float value)
    {
        if (value != 0)
        {
            Vector3 viewPos = main_camera.WorldToViewportPoint(transform.position);

            
            if (viewPos.y >= 0 && value <0 )
            {
                Debug.Log("Y:" + viewPos.y);
                transform.RotateAround(_RCenter.transform.position, Vector3.right, 20 * value * Time.deltaTime);
            }
            else
            {
                Debug.Log("Down Border of screen has reached"); 
            }

            if ( viewPos.y <= 0.5f && value > 0)
            {
                Debug.Log("Y:" + viewPos.y);
                transform.RotateAround(_RCenter.transform.position, Vector3.right, 20 * value * Time.deltaTime);
            }
            else
            {
                Debug.Log("Left Border of screen has reached"); 
            }
            
        }
    }
    
    
}
