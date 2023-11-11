using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KrubbsAssets
{
    public class DragItem : MonoBehaviour
    {
        public Text itemsDescription;

        Rigidbody hoveredRigidbody;
        Rigidbody draggedRigidbody;

        SpringJoint joint;
        float oldDrag;
        float oldAngularDrag;
        float savedDistance;

        // Use this for initialization
        void Start()
        {
            GameObject go = new GameObject("DragItem.cs Spring");
            Rigidbody body = go.AddComponent<Rigidbody>();
            joint = go.AddComponent<SpringJoint>();
            body.isKinematic = true;

            joint.transform.position = transform.position;
            joint.anchor = Vector3.zero;

            joint.spring = 50.0f;
            joint.damper = 5.0f;
            joint.maxDistance = 0.2f;
        }

        void Update()
        {
            if (!joint)
                Start();

            RaycastHit hitInfo = new RaycastHit();
            hoveredRigidbody = null;
            if (!draggedRigidbody && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 3f))
            {
                if (hitInfo.rigidbody != null && !hitInfo.rigidbody.isKinematic)
                    hoveredRigidbody = hitInfo.rigidbody;
            }

            if (hoveredRigidbody && !draggedRigidbody && Input.GetKeyDown(KeyCode.E))
            {
                draggedRigidbody = hoveredRigidbody;
                joint.connectedBody = draggedRigidbody;
                savedDistance = hitInfo.distance;
                joint.transform.position = hitInfo.point;
                joint.anchor = Vector3.zero;

                oldDrag = joint.connectedBody.drag;
                oldAngularDrag = joint.connectedBody.angularDrag;
                joint.connectedBody.drag = 10.0f;
                joint.connectedBody.angularDrag = 5.0f;
            }

            if (draggedRigidbody && !Input.GetKey(KeyCode.E))
            {
                joint.connectedBody.drag = oldDrag;
                joint.connectedBody.angularDrag = oldAngularDrag;
                joint.connectedBody = null;
                draggedRigidbody = null;
            }

            if (draggedRigidbody && joint.connectedBody != null)
            {
                joint.transform.position = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(savedDistance);
            }

            if (itemsDescription != null)
            {
                string text = "";
                if (draggedRigidbody != null)
                    text = draggedRigidbody.name;
                else if (hoveredRigidbody != null)
                    text = hoveredRigidbody.name;
                text = text.Replace(" Rigidbody", "").Replace(" ON", "").Replace(" OFF", "").Replace("(", "").Replace(")", "");

                if (draggedRigidbody != null)
                    text += " (Holding)";
                else if (hoveredRigidbody != null)
                    text += " (E)";

                char[] chars = text.ToCharArray();
                text = "";
                foreach(char temp in chars)
                {
                    if (!char.IsNumber(temp))
                        text += temp.ToString();
                }

                itemsDescription.text = text;
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!joint)
                Start();

            
        }
    }
}
