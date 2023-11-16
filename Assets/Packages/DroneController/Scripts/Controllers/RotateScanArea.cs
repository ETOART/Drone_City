using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScanArea : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject HitSphere;

    
    // Start is called before the first frame update
    void Start()
    {
        
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
        transform.RotateAround(_camera.transform.position, Vector3.up, 40 * value * Time.deltaTime);
    }
    
    public void moveScanAreaLEFTRIGHT(float value)
    {
       
        
        transform.RotateAround(_camera.transform.position, Vector3.right, 20 * value  * Time.deltaTime);
    }
    
    
}
