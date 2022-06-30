using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ReshiSoShy.Main
{
    public class ScenesCustomLoader : MonoBehaviour
    {
        List<AsyncOperation> _loadingOperations = new();
        [SerializeField]
        GameObject _loadingScreen;
        [SerializeField]
        GameObject _waitingForInputScreen;
        public static ScenesCustomLoader Instance;

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;
            LoadDirectly("MainMenu");
        }
        public void LoadDirectly(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
        public void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
        public void LoadSceneAsync(string sceneName)
        {
            OpenLoadingScreen();
            var asyncLoading =  SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncLoading.allowSceneActivation = false;
            _loadingOperations.Add(asyncLoading);
            StartCoroutine(WaitForLoadingAndClearLoadingScreen());
        }
        IEnumerator WaitForLoadingAndClearLoadingScreen()
        {
            var totalOperationWaiting = _loadingOperations.Count;
            var done = true;
            while (!done)
            {
                done = true;
                foreach (AsyncOperation loadingProcess in _loadingOperations)
                {
                    if (loadingProcess.progress < 0.9f)
                    {
                        done = false;
                    }
                }
                yield return null;
            }
            StartCoroutine(WaitForInput());
        }
        bool _continue = false;
        bool _waitingForInput = false;
        IEnumerator WaitForInput()
        {
            _waitingForInputScreen.SetActive(true);
            _waitingForInput = true;
            while (!_continue)
            {
                yield return null;
            }
            foreach (AsyncOperation loadingProcess in _loadingOperations)
            {
                loadingProcess.allowSceneActivation = true;
            }
            CloseLoadingScreen();
            _continue = false;
            _waitingForInput = false;
        }
        public void OpenLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }
        public void CloseLoadingScreen()
        {
            _loadingScreen.SetActive(false);
        }
        public void Continue()
        {
            _continue = true;
        }
        private void Update()
        {
            if (_waitingForInput)
            {
                if (Input.anyKeyDown)
                {
                    Continue();
                }
            }
        }
    }
}
