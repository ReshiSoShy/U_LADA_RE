using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Dialogues
{
    public class DialoguesQueuer : MonoBehaviour
    {
        public static DialoguesQueuer Instance;
        [SerializeField]
        TextAsset _allDialoguesFile;
        string[] _dialogueLines;
        TextDisplayer _textDisplayer;
        DialogueAudiosPlayer _dialogueAudiosPlayer;
        DialoguesQueuer _dialoguesQueuer;
        TriggersSolver _triggersSolver;

        Queue<Dialogue> _dialoguesQueue = new();
        public Queue<Dialogue> DialoguesQueue { get => _dialoguesQueue; private set => _dialoguesQueue = value; }

        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance);
            Instance = this;
            _textDisplayer = GetComponent<TextDisplayer>();
            _dialogueAudiosPlayer = GetComponent<DialogueAudiosPlayer>();
            _allDialoguesFile = GetComponent<TextAsset>();
            _triggersSolver = GetComponent<TriggersSolver>();
        }

        public void EnqueueNewDialogue(GameObject speaker, GameObject listener, string concept)
        {
            QueryCreator creator = new();
            string query = creator.CreateQuery(speaker, listener, concept);
            QuerySolver solver = new();
            RawPhraseLine phrase = solver.SolveQuery(query, speaker);
            int phraseIndex = phrase.GetPhraseID;
            string speakerName = phrase.CharName;
            List<int> triggers = phrase.GetTriggers();
            string rawNextAction = phrase.NextAction;
            string RawselectedPhrase = _dialogueLines[phraseIndex];
            string[] RawSelectedPhraseParts = RawselectedPhrase.Split('/');
            string phraseToBeDisplayed = RawSelectedPhraseParts[1];
            string phraseAudioID = RawSelectedPhraseParts[2];
            string audioStarT = RawSelectedPhraseParts[3];
            string audioEndT = RawSelectedPhraseParts[4];
            DialogueAudioLoader audioLoader = new();
            string localAudioPath = speakerName + '/' + phraseAudioID;
            AudioClip selectedAudio = audioLoader.FetchDialogueAudioFromResourcesFolder(localAudioPath);
            DialogueAudioEditor audioEditor = new();
            float audioStartingT = float.Parse(audioStarT);
            float audioEndingT = float.Parse(audioEndT);
            selectedAudio = audioEditor.StartEndAudioTrimmer(selectedAudio, audioStartingT, audioEndingT);
            Dialogue newDialogue = new(phraseToBeDisplayed, selectedAudio, triggers, rawNextAction);
            DialoguesQueue.Enqueue(newDialogue);
        }
    }
    [System.Serializable]
    public struct Dialogue
    {
        DialogueType _dialogueType;
        string _phraseText;
        AudioClip _audio;
        List<int> _triggers;
        string _rawNextAction;

        public Dialogue(string phraseText, AudioClip audio, List<int> triggers, string rawNextAction) : this()
        {
            _phraseText = phraseText;
            _audio = audio;
            _triggers = triggers;
            _rawNextAction = rawNextAction;
        }
    }
    public enum DialogueType
    {
        planeText,
        QuestionToPlayer
    }
}
