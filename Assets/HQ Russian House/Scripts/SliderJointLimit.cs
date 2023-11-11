using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KrubbsAssets
{
    public class SliderJointLimit : MonoBehaviour
    {
        public enum Axis {X, Y, Z }
        public Axis axis = Axis.X;
        public float min, max;
        // Use this for initialization
        void Start()
        {
            if(max < min)
            {
                Debug.Log("Changing Min and Max values..");
                float tempMax = max;
                max = min;
                min = tempMax;
            }
        }

        [ContextMenu("Set position to Min")]
        public void SetFromMin()
        {
            if (!transform.root)
                return;

            if (axis == Axis.X)
                transform.localPosition = new Vector3(min, transform.localPosition.y, transform.localPosition.z);
            if (axis == Axis.Y)
                transform.localPosition = new Vector3(transform.localPosition.x, min, transform.localPosition.z);
            if (axis == Axis.Z)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, min);
        }

        [ContextMenu("Set position to Max")]
        public void SetFromMax()
        {
            if (!transform.root)
                return;

            if (axis == Axis.X)
                transform.localPosition = new Vector3(max, transform.localPosition.y, transform.localPosition.z);
            if (axis == Axis.Y)
                transform.localPosition = new Vector3(transform.localPosition.x, max, transform.localPosition.z);
            if (axis == Axis.Z)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, max);
        }

        [ContextMenu("Set Min")]
        public void SetMin()
        {
            if (!transform.root)
                return;

            if (axis == Axis.X)
                min = transform.localPosition.x;
            if (axis == Axis.Y)
                min = transform.localPosition.y;
            if (axis == Axis.Z)
                min = transform.localPosition.z;
        }

        [ContextMenu("Set Max")]
        public void SetMax()
        {
            if (!transform.root)
                return;

            if (axis == Axis.X)
                max = transform.localPosition.x;
            if (axis == Axis.Y)
                max = transform.localPosition.y;
            if (axis == Axis.Z)
                max = transform.localPosition.z;
        }

        // Update is called once per frame
        void Update()
        {
            if (!transform.root)
                return;

            if (axis == Axis.X)
                transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, min, max), transform.localPosition.y, transform.localPosition.z);
            if (axis == Axis.Y)
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, min, max), transform.localPosition.z);
            if (axis == Axis.Z)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Clamp(transform.localPosition.z, min, max));
        }
    }
}
