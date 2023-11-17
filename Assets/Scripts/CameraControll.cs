using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class CameraControll : MonoBehaviour
{

    [SerializeField] private List<Camera> _cameras;
    [SerializeField] private List<Image> _images;
    [SerializeField] private GameObject debugUI;
    
    [SerializeField] private Camera _camera;
    
    [SerializeField] private TextMeshProUGUI _camera_name;
    
    [SerializeField] private TextMeshProUGUI _textMeshPro_x;
    [SerializeField] private TextMeshProUGUI _textMeshPro_y;
    [SerializeField] private TextMeshProUGUI _textMeshPro_w;
    [SerializeField] private TextMeshProUGUI _textMeshPro_h;

    [SerializeField] private int current_camera;


    public static bool debug;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //
    }

    public void fillData()
    {
        _camera_name.text = _cameras[ current_camera].gameObject.name;
        _textMeshPro_x.text = _cameras[ current_camera].rect.x+"";
        _textMeshPro_y.text = _cameras[ current_camera].rect.y+"";
        _textMeshPro_w.text = _cameras[ current_camera].rect.width+"";
        _textMeshPro_h.text = _cameras[ current_camera].rect.height+"";
        _images[current_camera].color = Color.red;
        
    }
    

    public void switchCamera(bool plus)
    {
        _images[current_camera].color = Color.white;
        if (plus)
        {
            current_camera += 1;
            if (current_camera >= _cameras.Count)
            {
                current_camera = 0;
            }
        }
        else
        {
            current_camera -= 1;
            if (current_camera <= 0 )
            {
                current_camera = _cameras.Count-1;
            }
        }
        
       
        
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)&&debug)
        {
            
            switchCamera(true);
            fillData();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            debug = !debug;
            debugUI.SetActive(debug);
            if(debug)fillData();
        }

        if (debug)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {

               Rect rect = _cameras[current_camera].rect;
               rect.x += 0.01f;
               _cameras[current_camera].rect = rect;
               
               
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                Rect rect = _cameras[current_camera].rect;
                rect.y += 0.01f;
                _cameras[current_camera].rect = rect;

            }
        
        
            if (Input.GetKey(KeyCode.UpArrow))  
            {
                Rect rect = _cameras[current_camera].rect;
                rect.width += 0.01f;
                _cameras[current_camera].rect = rect;

            
            }
        
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Rect rect = _cameras[current_camera].rect;
                rect.height += 0.01f;
                _cameras[current_camera].rect = rect;
            }
        }
    }
}
