using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Player
{
    public class GraphicsRotator : MonoBehaviour
    {
        [SerializeField]
        Transform _graphics;
        [SerializeField]
        float _lerpValue;
        public void RotateTowards(Vector3 movementVector)
        {
            var currentRot = _graphics.rotation;
            var targetRot = Quaternion.LookRotation(movementVector, Vector3.up);
            _graphics.rotation = Quaternion.Lerp(currentRot, targetRot, Time.deltaTime * _lerpValue);
        }
    }
}
