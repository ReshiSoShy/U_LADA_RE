using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReshiSoShy.Main.Data;
using ReshiSoShy.Main.CustomSettings;
namespace ReshiSoShy.Main.Dialogues
{
    /// <summary>
    /// Recolecta toda la información necesaria para hacer la llamada al <see cref="Listener"/>
    /// </summary>
    public class Caller : MonoBehaviour
    {
        public static Caller Instance;
        Listener _listener;
        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;

            _listener = GetComponent<Listener>();
        }
        public void StartConvo(GameObject from , GameObject to)
        {
            string petition = null;
            // This must all be stored in a gigantic dictionary and then
            // compared later
            var talkerData = from.GetComponent<CharacterData>();
            var receiberData = to.GetComponent<CharacterData>();
            string currentLang = "lengua:" + Settings.Instance.GetCurrentLanguage().ToString();
            string who = "soy:" + talkerData.GetName();
            string talkingWith = "con:" + receiberData.GetName();
            string talkerStoredData = talkerData.GetStoredData();
            string talkerProceduralData = talkerData.GetProceduralData();
            // WORLD DATA
            string worldData = "WORLD DATA";
            petition = currentLang + ',' +
                       who + ',' +
                       "concepto:" +"hablar" + ',' +
                       // Apartir de aquí se tratan como pares de valores
                       talkingWith + ',' +
                       talkerStoredData + ',' +
                       talkerProceduralData + ',' +
                       worldData;
            // Get rules
            var talkerRules = talkerData.GetRules();
            _listener.Push(petition, talkerRules, talkerData.GetName());
        }
        public void Speak(string keyValues)
        {
            // Target:Concept
            string[] bits = keyValues.Split(':');
            if (bits[0] == "null")
                return;
            var talkerGO = DialoguesManager.Instance.GetSpeaker(bits[0]);
            CharacterData talkerData = talkerGO.GetComponent<CharacterData>();
            string petition = null;
            // This must all be stored in a gigantic dictionary and then
            // compared later
            string currentLang = "lengua:" + Settings.Instance.GetCurrentLanguage().ToString();
            string who = "soy:" + talkerData.GetName();
            string talkerStoredData = talkerData.GetStoredData();
            string talkerProceduralData = talkerData.GetProceduralData();
            // WORLD DATA
            string worldData = "WORLD DATA";
            petition = currentLang + ',' +
                       who + ',' +
                       "concepto:" + bits[1]+ ',' +
                       // Apartir de aquí se tratan como pares de valores
                       talkerStoredData + ',' +
                       talkerProceduralData + ',' +
                       worldData;
            // Get rules
            var talkerRules = talkerData.GetRules();
            _listener.Push(petition, talkerRules, talkerData.GetName());
        }
    }
}               
