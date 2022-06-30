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
        [SerializeField]
        float _recenteringTimeThreshold;
        float _inactiveInputTime = 0.00f;
        [SerializeField]
        float _recenteringLerpValue;
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        Vector2 mouseInput;
        private void Update()
        {
            var xMouse = Input.GetAxis("Mouse X");
            var yMouse = Input.GetAxis("Mouse Y");
            mouseInput = new Vector2(xMouse, yMouse);
            if(mouseInput != Vector2.zero)
            {
                ActiveMovement();
                _inactiveInputTime = 0.00f;
            }
            else
                Recentering();
        }
        void ActiveMovement()
        {
            _yaw += mouseInput.x * _sens * Time.deltaTime;
            _pitch += mouseInput.y * _sens * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, -_pitchLimitAngle, _pitchLimitAngle);
            transform.rotation = Quaternion.Euler(new Vector3(_pitch, _yaw, 0));
        }
        void Recentering()
        {
            _inactiveInputTime += Time.deltaTime;
            if (_inactiveInputTime >= _recenteringTimeThreshold)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_forwardRef.forward), _recenteringLerpValue * Time.deltaTime);
                _pitch = transform.rotation.eulerAngles.x;
                _yaw = transform.rotation.eulerAngles.y;
                _pitch = Mathf.Abs(_pitch);
                if (_pitch > 180 && _pitch < 270)
                {
                    _pitch -= 180;
                }
                else if (_pitch > 270 && _pitch < 360)
                    _pitch = -(360 - _pitch);
            }
         
        }
    }
}
