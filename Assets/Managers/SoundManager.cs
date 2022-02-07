using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public static void Play(string name)
    {
        Sound s = Array.Find(Instance.sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning(name + " Sound not found");
            return;
        }
        if (s.source.isPlaying)
        {
            return;
        }
        s.source.Play();
    }

    public static void Stop(string name)
    {
        Sound s = Array.Find(Instance.sounds, sound => sound.name == name);
        {
            if(s == null)
            {
                Debug.LogWarning(name + " Sound not found");
                return;
            }
            s.source.Stop();
        }
    }

    public static void SetVolume(float value)
    {
        AudioListener.volume = value / 100;
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,3f)]
    public float pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}