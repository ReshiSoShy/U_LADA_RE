using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Cameras
{
    public class CameraBaseRotation : MonoBehaviour
    {
        float _yaw = 0.00f;
        float _pitch = 0.00f;
        [SerializeField]
        float _sens = 90;
        [SerializeField]
        float _pitchLimitAngle = 60;
        [SerializeField]
        float _yawLimitAngle = 90;
        [SerializeField]
        Transform _forwardRef;
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void Update()
        {
            _yaw += Input.GetAxis("Mouse X") * _sens * Time.deltaTime;
            _pitch += Input.GetAxis("Mouse Y") * _sens * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, -_pitchLimitAngle, _pitchLimitAngle);
            transform.rotation = Quaternion.Euler(new Vector3(_pitch, _yaw, 0));
        }
    }
}
