/// Made by Luca "*SeagullNoise*" SCHIFANO.
// Modified by Lucas "Gravity Tutel" ESPOSITO. :p

using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEditor;

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

    private string[] guids2;
    public AudioClip[] moansAudio;
    private string oldMoan;

    private string[] guids;
    public AudioClip[] moansAudioEnd;
    private string oldMoanEnd;

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
        guids2 = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/Sound/Moans" });
        moansAudio = new AudioClip[guids2.Length];
        int i = 0;
        foreach (string guid2 in guids2)
        {
            Debug.Log(AssetDatabase.GUIDToAssetPath(guid2));
            moansAudio[i] = (AudioClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid2), typeof(AudioClip));
            i++;
        }
        oldMoan = "BaseMoan";

        guids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/Sound/MoanEnd" });
        moansAudioEnd = new AudioClip[guids.Length];
        int y = 0;
        foreach (string guid in guids)
        {
            Debug.Log(AssetDatabase.GUIDToAssetPath(guid));
            moansAudioEnd[y] = (AudioClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(AudioClip));
            y++;
        }
        oldMoanEnd = "BaseMoanEnd";

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

    public void ChangeMoan()
    {
        AudioSource[] audioS;
        audioS = GetComponents<AudioSource>();
        foreach (Sound s in sounds)
          {

             if (s.source.clip.name == oldMoan)
               {
                   int rndMoan;
                   rndMoan = Random.Range(0, moansAudio.Length);
                 audioS[5].clip = moansAudio[rndMoan];
                 // Debug.Log(sounds.Length);
                 s.clip = moansAudio[rndMoan];
                   Debug.Log("Moan changed!");
                oldMoan = s.source.clip.name;
                // Debug.Log("TEST" + audioS[5].clip.name);

               }
             // Debug.Log(s.name); 

        } 

    }

    public void ChangeMoanEnd()
    {
        AudioSource[] audioS;
        audioS = GetComponents<AudioSource>();
        foreach (Sound s in sounds)
        {

            if (s.source.clip.name == oldMoanEnd)
            {
                int rndMoan;
                rndMoan = Random.Range(0, moansAudioEnd.Length);
                audioS[11].clip = moansAudioEnd[rndMoan];
                // Debug.Log(sounds.Length);
                s.clip = moansAudioEnd[rndMoan];
                Debug.Log("Moan end changed!");
                oldMoanEnd = s.source.clip.name;
                // Debug.Log("TEST" + audioS[5].clip.name);

            }
            // Debug.Log(s.name); 

        }

    }
}