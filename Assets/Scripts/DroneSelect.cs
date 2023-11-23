using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class DroneSelect : MonoBehaviour
{
    #region Props

    
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject selectedDrone;
    [SerializeField] private CanvasGroup _blackScreen;

    [SerializeField] private AudioSource _droneSelectSound;
    
    
    
    [SerializeField]
    private int currentIndex;
    [SerializeField] 
    private List<GameObject> elements;
    int temp;
    
    private GameObject currentCard;
    private LTDescr currentLtDescr;
    
    private Vector2 begin;
    public bool animate = true; 
    private bool debug = false;
    #endregion


    [SerializeField] private float prevTemp;
    
    [SerializeField] private InputActionReference _inputScanAimUPDOWN = default;
    [SerializeField] private InputActionReference _inputScanAimLEFTRIGHT = default;
    
    [SerializeField] private InputActionReference _inputScanAimAction= default;
    
    // Start is called before the first frame update
    void Start()
    {
        
        currentIndex = 0;
        
    }
    
    public void onPosition()
    {
     //   Debug.Log("OnPosition :"+currentCard.name);
     
        GameObject card = elements[temp];
        Vector3 position = card.transform.position;
        position.x = 0;
        card.transform.position = position;
        
        currentLtDescr = null;
    }

    public void move(int dir)
    {

       
       int nextIndex;
            
        
       nextIndex = currentIndex + dir;
         
         
       if (nextIndex < 0)
       {
           nextIndex = elements.Count - 1;
       }
       
       if (nextIndex > elements.Count-1)
       {
           nextIndex = 0;
       }
       currentIndex = nextIndex;
        _virtualCamera.LookAt = elements[nextIndex].transform;
        _virtualCamera.Follow = elements[nextIndex].transform;
        _animator.Play("SimpleRotate", -1, 0f);

        Debug.Log(currentIndex);

    }

    private void animateMove(int current, int next)
    {
        LeanTween.alphaCanvas(elements[current].GetComponent<CanvasGroup>(), 0, 0.2f).setEaseLinear();
        LeanTween.alphaCanvas(elements[next].GetComponent<CanvasGroup>(), 1, 0.2f).setEaseLinear();
        
        
    }




    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move(-1);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move(1);
        }
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DroneSelected();
        }


      
        float leftUpCross = _inputScanAimLEFTRIGHT.action.ReadValue<float>();
        if (leftUpCross != prevTemp)
        {
            prevTemp = leftUpCross;
            if (leftUpCross > 0)
            {
                move(1);
            }

            if (leftUpCross < 0)
            {
                move(-1);
            } 
            
        }
       
        
        
            
            
            
        if (_inputScanAimAction.action.ReadValue<float>()!=0)
        {
            Debug.Log("A PRESSED");
            DroneSelected();
        }

        
        
    }

    public void DroneSelected()
    {
        _droneSelectSound.Play();
        Debug.Log("Selected");
        PlayerPrefs.SetInt("DroneSelected",currentIndex);
        _animator.SetBool("DroneSelected",true);
            
        elements[currentIndex].GetComponentInChildren<Animator>().SetBool("DroneFly",true);
        LeanTween.alphaCanvas(_blackScreen, 1, 2f).setDelay(2f).setEaseLinear().setOnComplete(StartGameLevel);

    }

    public void StartGameLevel()
    {
        SceneManager.LoadScene("S_03_Villlage");
    }
    
    
}
