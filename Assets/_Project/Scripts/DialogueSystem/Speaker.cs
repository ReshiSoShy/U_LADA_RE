using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReshiSoShy.Main.Data;
namespace ReshiSoShy.Main.Dialogues
{
    public class Speaker : MonoBehaviour
    {
        CharacterData _myData;
        public bool IsTalking = false;
        private void Awake()
        {
            _myData = GetComponent<CharacterData>();
        }
        private void OnEnable()
        {
            DialoguesManager.Instance.RegisterSpeaker(this);
        }
        private void OnDisable()
        {
            DialoguesManager.Instance.RemoveSpeaker(this);
        }
        public void Speak(string arguments)
        {
            string petition = "";
            var canTalk = DialoguesManager.Instance.AskForTalkingTurn(petition);
            if (canTalk)
                IsTalking = true;
        }
        public string GetName() => _myData.GetName();
    }
}
