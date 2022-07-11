using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Dialogues
{
    public class TriggersSolver : MonoBehaviour
    {
        List<int> _loadedTriggers = new();
        public void LoadTriggers(List<int> triggersList)
        {
            _loadedTriggers.Clear();
            foreach (int triggID in triggersList)
                _loadedTriggers.Add(triggID);
        }
        public void SolveLoadedTriggers()
        {

        }
    }
}
