using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Dialogues
{
    public class DialogueAudioLoader 
    {
        public AudioClip FetchDialogueAudioFromResourcesFolder(string audioPath) => Resources.Load(audioPath) as AudioClip;
       
    }
}