using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.audio_source = gameObject.AddComponent<AudioSource>();
            s.audio_source.clip = s.clip;

            s.audio_source.volume = s.volume;
            s.audio_source.pitch = s.pitch;
            s.audio_source.loop = s.loop;
        }
    }

    public void Start()
    {
        
    }

    public void PlayAudio (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.audio_source.Play();
    }

    public AudioSource Search(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.audio_source;
    }
}
