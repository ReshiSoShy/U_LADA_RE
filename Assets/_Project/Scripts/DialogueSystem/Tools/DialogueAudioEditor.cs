using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReshiSoShy.Main.Dialogues
{
    public class DialogueAudioEditor
    {
        public AudioClip StartEndAudioTrimmer(AudioClip audioclip, float startingTime, float endingTime)
        {
            int frequency = audioclip.frequency;
            float timeLength = endingTime - startingTime;
            int samplesLength = (int)(frequency * timeLength) * audioclip.channels;
            AudioClip newClip = AudioClip.Create(audioclip.name, samplesLength, 1, frequency, false);
            float[] data = new float[samplesLength];
            audioclip.GetData(data, (int)(frequency * startingTime));
            newClip.SetData(data, 0);
            return newClip;
        }
    }
}
