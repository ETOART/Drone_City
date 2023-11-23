using System;
using UnityEngine;

namespace DroneController
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Project References:")]
        [SerializeField] private CameraMovementData _cameraMovementData ;
        [Header("Scene References:")]
        [SerializeField] private Transform _objecToFollow; //our drone game object

        private Vector3 _positionVelocity = default;
        private float _cameraTiltRotation = default;
        private float _previousFrameCameraPosition = default;


        private void Start()
        {
            _previousFrameCameraPosition = transform.position.y;
            
        }


        protected virtual void FixedUpdate()
        {
            FollowDroneMethod();
            TiltCameraUpDown();
            ApplyCameraRotation();
        }

        private void FollowDroneMethod()
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                _objecToFollow.TransformPoint(_cameraMovementData.Offset),
                ref _positionVelocity,
                _cameraMovementData.FollowSpeed);
        }

        private void TiltCameraUpDown()
        {
            _cameraTiltRotation = Mathf.Lerp(
                _cameraTiltRotation,
                (transform.position.y - _previousFrameCameraPosition) * -_cameraMovementData.YFollowStrength,
                Time.deltaTime * 10);
            _previousFrameCameraPosition = transform.position.y;
        }

        private void ApplyCameraRotation()
        {
            transform.rotation = Quaternion.Euler(
                0 + _cameraTiltRotation,
                _objecToFollow.rotation.eulerAngles.y,
                0);
        }
    }
}
