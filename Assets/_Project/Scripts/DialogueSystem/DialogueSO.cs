using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Dialogues
{
    public class DialogueSO : ScriptableObject
    {
        [SerializeField]
        TextAsset _dialogueText;
        private void OnValidate()
        {
            var content = _dialogueText.text;

        }
    }
}
