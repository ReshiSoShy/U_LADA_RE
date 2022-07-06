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
        Listener _listener;
        private void Awake()
        {
            _listener = GetComponent<Listener>();
        }
        public void Speak(GameObject from , GameObject to, Concepts concpt)
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
                       "concepto:" +concpt.ToString() + ',' +
                       // Apartir de aquí se tratan como pares de valores
                       talkingWith + ',' +
                       talkerStoredData + ',' +
                       talkerProceduralData + ',' +
                       worldData;
            // Get rules
            var talkerRules = talkerData.GetRules();
            _listener.Push(petition, talkerRules);
            
        }
    }
    public enum Concepts
    {
        hablar
    }
}
//petition = currentLang.ToString() + '/' +
//                       who + '/' +
//                       concpt.ToString() + '/' +
//                       talkingWith + '/' +
//                       talkerStoredData + '/' +
//                       talkerProceduralData + '/' +
//                       worldData;
