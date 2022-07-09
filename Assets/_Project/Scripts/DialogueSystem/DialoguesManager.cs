using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Dialogues
{
    public class DialoguesManager : MonoBehaviour
    {
        public static DialoguesManager Instance;
        bool _listeningForSpeakers = true;
        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;
        }
        List<Speaker> _availableSpeakers = new();
        public void RegisterSpeaker(Speaker newSpeaker)
        {
            if (!_availableSpeakers.Contains(newSpeaker))
                _availableSpeakers.Add(newSpeaker);
        }
        public void RemoveSpeaker(Speaker removedSpeaker)
        {
            if (_availableSpeakers.Contains(removedSpeaker))
                _availableSpeakers.Remove(removedSpeaker);
        }
        public bool AskForTalkingTurn(string petition)
        {
            return _listeningForSpeakers;
        }
        public GameObject GetSpeaker(string name)
        {
            foreach(Speaker speaker in _availableSpeakers)
            {
                if (speaker.GetName() == name)
                    return speaker.gameObject;
            }
            return null;
        }
    }
}
