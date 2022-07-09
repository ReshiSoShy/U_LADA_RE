using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Data
{
    [CreateAssetMenu(fileName = "New Character Data", menuName = "SO/Character Data")]
    public class CharacterDataSO : ScriptableObject
    {
        [SerializeField]
        string _characterName;
        [SerializeField]
        TextAsset _rules;
        public string GetCharacterName => _characterName;
        public string GetRules()
        {
            return _rules.text;
        }
    }
}
