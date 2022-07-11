using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace ReshiSoShy.Main.Dialogues
{
    public class TextDisplayer : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _speakerNameTextField;
        [SerializeField]
        TextMeshProUGUI _speakerContentTextField;
        public void DisplaySpeakerName(string name)
        {
            _speakerContentTextField.text = name;
        }
        public void DisplaySpeakerContent(string content)
        {
            _speakerContentTextField.text = content;
        }
    }
}
