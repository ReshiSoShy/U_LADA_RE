using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReshiSoShy.Main.Data;
using ReshiSoShy.Main.CustomSettings;
using System;
namespace ReshiSoShy.Main.Dialogues
{
    public class Listener : MonoBehaviour
    {
        [SerializeField]
        TextAsset _dialogues;
        public void Push(string petition, string talkerRules)
        {
            string[] petitionKVPairs = petition.Split(',');
            Dictionary<string, string> petitionKeyValues = new();
            foreach(string kvPair in petitionKVPairs)
            {
                string[] bits = kvPair.Split(':');
                if(bits.Length == 2)
                {
                    string key = bits[0];
                    string value = bits[1];
                    petitionKeyValues.Add(key, value);
                }
            }
            string[] rules = talkerRules.Split('\n');
            int highestScore = 0;
            List<string> winnerRules = new();
            foreach (string rule in rules)
            {
                int ruleScore = 0;
                if (rule[0] == '/' && rule[1] == '/' )
                {
                }
                else
                {
                    string[] ruleBits = rule.Split('/');
                    string id = ruleBits[0];
                    string[] pairs = ruleBits[1].Split(',');
                    Dictionary<string, string> rulesKeyValues = new();
                    foreach(string KV in pairs)
                    {
                        string[] bits = KV.Split(':');
                        string key = bits[0];
                        string value = bits[1];
                        rulesKeyValues.Add(key, value);
                    }
                    foreach (KeyValuePair<string, string> keyV in rulesKeyValues)
                    {
                        if (petitionKeyValues.ContainsKey(keyV.Key))
                        {
                            var desiredValue = petitionKeyValues[keyV.Key];
                            if(keyV.Value == desiredValue)
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
                        winnerRules.Add(rule);
                    }
                    else if (ruleScore == highestScore)
                        winnerRules.Add(rule);
                }
            }
            string ruleToUse = null;
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
            if (ruleToUse == null)
                return;
            string[] winnerBits = ruleToUse.Split('/');
            var triggers = winnerBits[2];
            var phrasesIds = winnerBits[3];

            // Now we have the rule with the greatest score, go ahead and manage what happens next
            // Call the triggers and say whatever it has to say
            ManageTriggers(triggers);
            ShowTextOnScreen(phrasesIds);
        }
        void ManageTriggers(string triggersIds)
        {
            string[] triggersBits = triggersIds.Split(',');
            foreach (string trigg in triggersBits)
                print("Calling for trigger wit id : " + trigg);
        }
        void ShowTextOnScreen(string phrasesIds)
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
                    Debug.Log("We printing phrase with id : " + index);
                }
                else
                {
                    Debug.LogError("There was a problem with the text ids convertion, failed to convert : " + id);
                }
            }
        }
    }
}
