using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.CustomSettings
{
    public class Settings : MonoBehaviour
    {
        public static Settings Instance;
        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;
        }
        [SerializeField]
        SettingsSO _settingsProfile;
        public void SetLanguage(Languages newLanguage) => _settingsProfile.SetLanguage(newLanguage);
        public Languages GetCurrentLanguage() => _settingsProfile.GetCurrentLanguage();
    }
}