﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class FocusSphere : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private Color mainColor;
    [SerializeField] private Color hoverColor;


    [SerializeField] private bool readyToCheck;
    [SerializeField] private GameObject scanObject;

    [SerializeField] private GameObject crossObject;
    [SerializeField] private bool block;

    [SerializeField] private AudioSource _scanSound;
    [SerializeField] private AudioSource _wrongScanTarget;


    public float raycastDistance = 100f; // Длина луча для рейкаста
    private Transform hitObject; // Объект, с которым пересекается луч

    Material mat;
    private void Start()
    {
        mat = new Material(renderer.material);
    }
    void Update()
    {
        Vector3 raycastOrigin = transform.position;
        Vector3 raycastDirection = transform.up;

        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastDistance))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Object"))
            {
                hitObject = hit.transform;
                if (hitObject.CompareTag("done")) return;

                scanObject = hitObject.gameObject;
                readyToCheck = true;
                ChangeColor(hoverColor);

            }
            
        }
        else
        {
            ChangeColor(mainColor);
            hitObject = null;
        }

        //// Отрисуйте луч для визуализации
        Debug.DrawRay(raycastOrigin, raycastDirection * raycastDistance, Color.red);

        if (hitObject != null)
        {
            Debug.Log("Пересечение с объектом: " + hitObject.name);
        }
    }

    private void ChangeColor(Color color)
    {
        mat.color = color;
        renderer.material = mat;
    }
    public void ScanObject()
    {

        if (readyToCheck && scanObject != null && !block && !scanObject.tag.Equals("done"))
        {

            scanObject.tag = "done";

            block = true;
            GameObject cross = Instantiate(crossObject, scanObject.transform);

            _scanSound.Play();
            GameManager.instance.AddScore();
            GameManager.instance.ShowScanTargetData(scanObject);

            LeanTween.moveY(cross, cross.transform.position.y + 25, 3).setEaseOutCirc().setOnComplete((o =>
            {
                //Destroy(cross);
                block = false;

            }));

        }
        else
        {
            _wrongScanTarget.Play();
        }


    }


    public void moveScanAreaUPDOWN(float value)
    {
        
        Vector3 anglesNow = transform.localEulerAngles;
        Debug.Log(anglesNow);
        if (anglesNow.x < 85 && value > 0)
            transform.localEulerAngles = new Vector3((anglesNow.x + value), anglesNow.y, anglesNow.z);
        if (anglesNow.x > 50 && value < 0)
            transform.localEulerAngles = new Vector3((anglesNow.x + value), anglesNow.y, anglesNow.z);
    }
    public void moveScanAreaLEFTRIGHT(float value)
    {
        Vector3 anglesNow = transform.localEulerAngles;

        if (anglesNow.y > 170 && value <0)
            transform.localEulerAngles = new Vector3(anglesNow.x, (anglesNow.y + value), anglesNow.z);
        if(anglesNow.y < 250 && value > 0)
            transform.localEulerAngles = new Vector3(anglesNow.x, (anglesNow.y + value), anglesNow.z);
    }

}




