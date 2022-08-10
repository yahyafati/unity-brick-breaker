using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public Sound[] sounds;
    public Dictionary<string, Sound> soundMap;

    public void Awake()
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
        DontDestroyOnLoad(this.gameObject);
        soundMap = new Dictionary<string, Sound>(sounds.Length * 2);
        Array.ForEach(sounds, (sound) =>
        {
            sound.source = this.gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            soundMap.Add(sound.name, sound);
        });
    }

    public void Start()
    {
        Debug.Log("Playing theme song");
        Play("Theme");
    }

    public void Play(string name, bool stopPrev = true)
    {
        // Sound sound = Array.Find(sounds, (sound) => sound.name == name);
        Sound sound = soundMap[name];
        Debug.Log("SoundFound: " + sound != null);
        if (stopPrev && sound.source.isPlaying)
        {
            sound.source.Stop();
        }
        sound.source.Play();

    }
}
