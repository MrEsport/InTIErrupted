/// Made by Luca "*SeagullNoise*" SCHIFANO.
// Modified by Lucas "Gravity Tutel" ESPOSITO. :p

using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundTransmitter : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public AudioMixerGroup audioMixerGroup;

        [Range(0f, 1f)]
        public float volume = 0.1f;
        [Range(0f, 2f)]
        public float pitch = 1.0f;
        [Range(-1f, 1f)]
        public float pan = 0f;

        public bool playOnAwake = false;
        public bool loop = false;

        [HideInInspector]
        public AudioSource source;
    }

    private static SoundTransmitter _instance;
    public static SoundTransmitter Instance { get => _instance; }
    
    public Sound[] sounds;

    void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
            
            s.source.volume = s.volume;
            
            s.source.pitch = s.pitch;

            s.source.panStereo= s.pan;

            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.loop;
            
            if(s.playOnAwake) Play(s.name);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Play();
    }

    public IEnumerator PlayWDelay(string name, float time)
    {
        yield return new WaitForSeconds(time);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Pause();
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }
}