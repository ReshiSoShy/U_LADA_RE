using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        CharacterController _controller;
        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }
        public void Move(Vector3 movementVector)
        {
            _controller.Move(movementVector);
        }
    }
}
