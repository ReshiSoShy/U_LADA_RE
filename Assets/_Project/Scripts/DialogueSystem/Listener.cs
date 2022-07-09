using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReshiSoShy.Main.Data;
using ReshiSoShy.Main.CustomSettings;
using TMPro;
using System;
namespace ReshiSoShy.Main.Dialogues
{
    public class Listener : MonoBehaviour
    {
        [SerializeField]
        TextAsset _dialogues;
        AudioSolver _audioSolver;
        Caller _caller;
        Queue<Phrase> _phrases = new();
        private void Awake()
        {
            _audioSolver = GetComponent<AudioSolver>();
            _triggerSolver = GetComponent<TriggersSolver>();
            _caller = GetComponent<Caller>();
        }
        private void Update()
        {
            if(_phrases.Count != 0)
            {
                if (!_audioSolver.IsPlaying())
                {
                    if (Input.GetKeyDown(KeyCode.Return) || _audioSolver.Listening)
                    {
                        var phrase = _phrases.Dequeue();
                        ManageTriggers(phrase.Triggers);
                        SyncTextAndAudio(phrase.CharName, phrase.PhrasesIds, phrase.NextAction);
                        _audioSolver.Listening = false;
                    }
                }
            }
        }
        public void Push(string petition, string talkerRulesRaw, string charName)
        {
            Debug.Log("We pushing petition : " + petition);
            string[] petitionKVPairs = petition.Split(',');
            var petitionKeyValues = StringParsingFunctions.TurnIntoDictionary(petitionKVPairs, ':');
            List<string> winnerRules = SelectWinnerRules(petitionKeyValues, talkerRulesRaw);
            string ruleToUse = null;
            ruleToUse = SelectOneRuleOnly(winnerRules, ruleToUse);
            if (ruleToUse == null)
                return;
            string[] winnerBits = ruleToUse.Split('/');
            var triggers = winnerBits[2];
            var phrasesIds = winnerBits[3];
            var nextAction = winnerBits[4];
            Phrase newPhrase = new Phrase(charName, triggers, phrasesIds, nextAction);
            _phrases.Enqueue(newPhrase);
        }

        private void SyncTextAndAudio(string charName, string phrasesIds, string nextAction)
        {
            string allContent = _dialogues.text;
            // Make a class that clears out the empty and the comment lines
            // Make an automatic way of handling ids
            string[] lines = allContent.Split('\n');
            ShowTextOnScreen(phrasesIds, charName);
            // this should wait until the last sentence has finished
            HandleNextAction(nextAction);
        }

        private static string SelectOneRuleOnly(List<string> winnerRules, string ruleToUse)
        {
            if (winnerRules.Count > 1)
            {
                var randomInt = UnityEngine.Random.Range(0, winnerRules.Count);
                ruleToUse = winnerRules[randomInt];
            }
            else if (winnerRules.Count == 1)
            {
                ruleToUse = winnerRules[0];
            }
            else
            {

            }
            return ruleToUse;
        }

        private static List<string> SelectWinnerRules(Dictionary<string, string> contextData, string talkerRulesRaw)
        {
            string[] criterions = talkerRulesRaw.Split('\n',StringSplitOptions.RemoveEmptyEntries);
            criterions = StringParsingFunctions.ClearCommentLines(criterions);
            int highestScore = 0;
            List<string> winnerRules = new();
            foreach (string criterion in criterions)
            {
                int ruleScore = 0;
                string[] ruleBits = criterion.Split('/');
                string id = ruleBits[0];
                string[] pairs = ruleBits[1].Split(',');
                var rulesKeyValues = StringParsingFunctions.TurnIntoDictionary(pairs, ':');
                foreach (KeyValuePair<string, string> keyV in rulesKeyValues)
                {
                    if (contextData.ContainsKey(keyV.Key))
                    {
                        var desiredValue = contextData[keyV.Key];
                        if (keyV.Value == desiredValue)
                        {
                            ruleScore++;
                        }
                        else
                        {
                            ruleScore = -1;
                            break;
                        }
                    }
                    else
                    {
                        // Missmatch, rule discarted
                        ruleScore = -1;
                        break;
                    }
                }
                if (ruleScore == -1)
                    continue;
                if (ruleScore > highestScore)
                {
                    winnerRules.Clear();
                    highestScore = ruleScore;
                    winnerRules.Add(criterion);
                }
                else if (ruleScore == highestScore)
                    winnerRules.Add(criterion);
            }

            return winnerRules;
        }

        TriggersSolver _triggerSolver;
        void ManageTriggers(string triggersIds)
        {
            string[] triggersBits = triggersIds.Split(',');
            foreach (string trigg in triggersBits)
            {
                _triggerSolver.SolveForTrigger(trigg);
            }
        }
        [SerializeField]
        TextMeshProUGUI _contentTextField;
        [SerializeField]
        TextMeshProUGUI _speakerNameTextField;
        void ShowTextOnScreen(string phrasesIds, string charName)
        {
            string[] phrasesIdsBits = phrasesIds.Split(',');
            // Read from the script where all the phrases are lying down, for each character 
            // with every language, by id so we are blasing fast asf
            string allContent = _dialogues.text;
            // Make a class that clears out the empty and the comment lines
            // Make an automatic way of handling ids
            string[] lines = allContent.Split('\n');
            foreach(string id in phrasesIdsBits)
            {
                int index;
                bool wasPossible = Int32.TryParse(id, out index);
                if (wasPossible)
                {
                    var phrase = lines[index];
                    string[] bits = phrase.Split('/');
                    string content = bits[1];
                    string audioID = bits[2];
                    string audioStartingPoint = bits[3];
                    string audioEndPoint = bits[4];
                    // To do : wait for audio to be ready and display and play audio at same time.
                    _audioSolver.PushAudio(charName,audioID, audioStartingPoint, audioEndPoint);
                    PrintOnScreen(content);
                    _speakerNameTextField.text = SolveName(charName);
                }
                else
                {
                    Debug.LogError("There was a problem with the text ids convertion, failed to convert : " + id);
                }
            }
        }
        void HandleNextAction(string nextAction)
        {
            _caller.Speak(nextAction);
        }
        public void PrintOnScreen(string content)
        {
            _contentTextField.text = content;
        }
        string SolveName(string name)
        {
            switch (name)
            {
                case "Arturin":
                    return "Arturín";
            }
            return name;
        }
    }
    struct Phrase
    {
        string charName;
        string triggers;
        string phrasesIds;
        string nextAction;

        public Phrase(string charName, string triggers, string phrasesIds, string nextAction)
        {
            this.charName = charName;
            this.triggers = triggers;
            this.phrasesIds = phrasesIds;
            this.nextAction = nextAction;
        }
        public string CharName => charName;
        public string Triggers => triggers;
        public string PhrasesIds => phrasesIds;
        public string NextAction => nextAction;
    }
}
