using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ReshiSoShy.Main.Data;
namespace ReshiSoShy.Main.Dialogues
{
    public class QuerySolver 
    {
        [SerializeField]
        TextAsset _allDialogues;
        void Awake()
        {
            string allContent = _allDialogues.text;
        }
        public RawPhraseLine SolveQuery(string query, GameObject speaker)
        {
            string[] petitionKVPairs = query.Split(',');
            var petitionKeyValues = StringParsingFunctions.TurnIntoDictionary(petitionKVPairs, ':');
            CharacterData speakerData = speaker.GetComponent<CharacterData>();
            string speakerRules = speakerData.GetRules();
            List<string> winnerRules = SelectWinnerRules(petitionKeyValues, speakerRules);
            string ruleToUse = null;
            ruleToUse = SelectOneRuleOnly(winnerRules, ruleToUse);
            string[] winnerBits = ruleToUse.Split('/');
            var triggers = winnerBits[2];
            var phrasesIds = winnerBits[3];
            var nextAction = winnerBits[4];
            RawPhraseLine newPhrase = new(speakerData.GetName(), triggers, phrasesIds, nextAction);
            return newPhrase;
        }
        private List<string> SelectWinnerRules(Dictionary<string, string> contextData, string talkerRulesRaw)
        {
            string[] criterions = talkerRulesRaw.Split('\n', StringSplitOptions.RemoveEmptyEntries);
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
       
    }
    public struct RawPhraseLine
    {
        string charName;
        string triggers;
        string phraseId;
        string RawnextAction;

        public RawPhraseLine(string charName, string triggers, string phrasesIds, string nextAction)
        {
            this.charName = charName;
            this.triggers = triggers;
            this.phraseId = phrasesIds;
            this.RawnextAction = nextAction;
        }
        public string CharName => charName;
        public List<int> GetTriggers()
        {
            string[] triggers = this.triggers.Split(',');
            List<int> answer = new();
            foreach(string trigg in triggers)
            {
                try
                {
                    int triggerID = Int32.Parse(trigg);
                    answer.Add(triggerID);
                }
                catch
                {
                    continue;
                }
            }
            return answer;
        }
        public int GetPhraseID => Int32.Parse(phraseId);
        public string NextAction => RawnextAction;
    }
}
