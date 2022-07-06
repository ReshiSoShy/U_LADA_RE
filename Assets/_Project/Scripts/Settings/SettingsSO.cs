using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ReshiSoShy.Main.CustomSettings
{
    [CreateAssetMenu(fileName = "New Settings Profile", menuName = "SO/Settings")]
    public class SettingsSO : ScriptableObject
    {
        [SerializeField]
        Languages _currentLanguageUsed;
        public Languages GetCurrentLanguage() => _currentLanguageUsed;
        public void SetLanguage(Languages newLanguage) => _currentLanguageUsed = newLanguage;
    }
    public enum Languages
    {
        es,
        eng
    }
}
