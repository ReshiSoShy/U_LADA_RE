using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Data
{
    public class CharacterData : MonoBehaviour
    {
        [SerializeField]
        CharacterDataSO _charData;
        public string GetName() => _charData.GetCharacterName;
        public string GetStoredData()
        {
            return "STORED DATA";
        }
        public string GetProceduralData()
        {
            return "PROCEDURAL DATA";
        }
        public string GetRules()
        {
            return _charData.GetRules();
        }
    }
}