using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main
{
    public class GamePauserCaller : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Pause"))
            {
                GamePauser.Instance.Toggle();
            }
        }
    }
}
