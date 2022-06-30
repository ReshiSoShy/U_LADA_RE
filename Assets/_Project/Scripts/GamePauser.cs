using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main
{
    public class GamePauser : MonoBehaviour
    {
        public static GamePauser Instance;
        [SerializeField]
        GameObject _pauseMenu;
        bool _isPaused = false;

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;
        }
        void Pause()
        {
            Time.timeScale = 0.00f;
            _isPaused = true;
            _pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        void Resume()
        {
            Time.timeScale = 1.00f;
            _isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _pauseMenu.SetActive(false);
        }
        public void Toggle()
        {
            _isPaused = !_isPaused;
            if (_isPaused)
                Pause();
            else
                Resume();
        }
    }
}
