using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
namespace ReshiSoShy.Main.Dialogues
{
    public class DialogueAudiosPlayer : MonoBehaviour
    {
        GameObject _audiosPlayerGO;
        AudioSource _audioSource;
        private void Awake()
        {
            _audiosPlayerGO = new GameObject("Dialogue Audio Source");
            _audiosPlayerGO.transform.position = Vector3.zero;
            var audioSourceComponent = _audiosPlayerGO.AddComponent<AudioSource>();
            audioSourceComponent.playOnAwake = false;
            audioSourceComponent.spatialBlend = 1.00f;
            audioSourceComponent.rolloffMode = AudioRolloffMode.Linear;
            audioSourceComponent.maxDistance = 30;
            _audioSource = _audiosPlayerGO.GetComponent<AudioSource>();
        }
        public void PlayAudioAtPosition(AudioClip audio, Vector3 worldPosition)
        {   
            _audiosPlayerGO.transform.position = worldPosition;
            _audioSource.clip = audio;
            Play();
        }
        public bool IsPlaying()
        {
            return _audioSource.isPlaying;
        }
        public void Stop()
        {
            _audioSource.Stop();
        }
        public void Pause()
        {
            _audioSource.Pause();
        }
        public void Play()
        {
            _audioSource.Play();
        }
    }
}
