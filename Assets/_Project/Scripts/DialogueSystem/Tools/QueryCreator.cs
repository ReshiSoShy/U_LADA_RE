using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReshiSoShy.Main.Data;
using ReshiSoShy.Main.CustomSettings;
namespace ReshiSoShy.Main.Dialogues
{
    public class QueryCreator
    {
        const string _lenguageKey = "lengua";
        const string _speakerKey = "soy";
        const string _listenerKey = "con";
        const string _conceptKey = "concepto";
        CharacterData _speakerData;
        CharacterData _listenerData;
        string _concept = "null";
        public string CreateQuery(GameObject speaker, GameObject listener, string concept)
        {
            _speakerData = speaker.GetComponent<CharacterData>();
            _listenerData = listener.GetComponent<CharacterData>();
            string queryPetition = CreateKeyValuesPairs();
            _concept = concept;
            return queryPetition;
        }
        private string CreateKeyValuesPairs()
        {
            string result = "null";
            string currentLang = _lenguageKey + ':' + Settings.Instance.GetCurrentLanguage().ToString();
            string who = _speakerKey + ':' + _speakerData.GetName();
            string talkingWith = _listenerKey + ':' + _listenerData.GetName();
            string talkerStoredData = _speakerData.GetStoredData();
            string talkerProceduralData = _speakerData.GetProceduralData();
            string worldData = "WORLD DATA";
            result = currentLang + ',' +
                       who + ',' +
                       _conceptKey + ',' +
                       _concept + ',' +
                       talkingWith + ',' +
                       talkerStoredData + ',' +
                       talkerProceduralData + ',' +
                       worldData;
            return result;
        }
    }
}
