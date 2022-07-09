using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
namespace ReshiSoShy.Main.Dialogues
{
    public class AudioSolver : MonoBehaviour
    {
        [SerializeField]
        AudioSource _audioPlayer;
        public bool IsPlaying()
        {
            return _audioPlayer.isPlaying;
        }
        public bool Listening = true;
       public void PushAudio(string charName, string audioID, string audioStartingPoint, string audioEndingPoint)
        {
            bool parsedCorrectly = Int32.TryParse(audioID, out int index);
            float startingPoint = float.Parse(audioStartingPoint,CultureInfo.InvariantCulture.NumberFormat);
            float endingPoint = float.Parse(audioEndingPoint, CultureInfo.InvariantCulture.NumberFormat);
            if (parsedCorrectly)
            {
                Debug.Log("Parsed audio id correctly");
                AudioClip audioClip = Resources.Load(charName + '/' + audioID) as AudioClip;
                if (audioClip == null)
                    Debug.LogError("Audio not found");

                else
                {
                    var subClip = MakeSubclip(audioClip, startingPoint, endingPoint);
                    _audioPlayer.PlayOneShot(subClip);
                }
            }
            else
            {
                Debug.LogError("Error parsing at audio");
            }
        }
        private AudioClip MakeSubclip(AudioClip clip, float start, float stop)
        {
            Debug.Log($"Starting point at { start} and ending point at : {stop}");
            /* Create a new audio clip */
            int frequency = clip.frequency;
            float timeLength = stop - start;
            int samplesLength = (int)(frequency * timeLength) * clip.channels;
            AudioClip newClip = AudioClip.Create(clip.name + "-sub", samplesLength, 1, frequency, false);
            /* Create a temporary buffer for the samples */
            float[] data = new float[samplesLength];
            /* Get the data from the original clip */
            clip.GetData(data, (int)(frequency * start));
            /* Transfer the data to the new clip */
            newClip.SetData(data, 0);
            /* Return the sub clip */
            return newClip;
        }

    }
}
