using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.ScenesControls
{
    public class ScenesCaller : MonoBehaviour
    {
        public void LoadSceneAsync(string sceneName)
        {
            ScenesCustomLoader.Instance.LoadSceneAsync(sceneName);

        }
        public void LoadDirectly(string sceneName)
        {
            ScenesCustomLoader.Instance.LoadDirectly(sceneName);
        }
        public void Unload(string sceneName)
        {
            ScenesCustomLoader.Instance.UnloadScene(sceneName);
        }

    }
}
