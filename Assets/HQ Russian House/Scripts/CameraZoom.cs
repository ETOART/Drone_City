using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KrubbsAssets
{
    [RequireComponent(typeof(Camera))]
    public class CameraZoom : MonoBehaviour
    {
        float startfov;
        public float targetFOV;
        Camera mycam;

        // Use this for initialization
        void Start()
        {
            mycam = GetComponent<Camera>();
            startfov = mycam.fieldOfView;
        }

        // Update is called once per frame
        void Update()
        {
            mycam.fieldOfView = Mathf.Lerp(mycam.fieldOfView, Input.GetMouseButton(1) ? 30 : startfov, Time.deltaTime * 5);

        }


    }

}