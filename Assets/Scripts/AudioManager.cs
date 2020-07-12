using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    public float timeSinceLastSound = 0.0f, timeBetweenSounds = 15.0f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.vol;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //Run Theme
        Play("Theme");
    }

    private void Update()
    {
        timeSinceLastSound += Time.deltaTime;

        if(timeSinceLastSound > timeBetweenSounds)
        {
            timeSinceLastSound = 0;
            Play("" + UnityEngine.Random.Range(1, 33));
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.audioName == name);
        if (s == null)
            return;
        s.source.Play();
    }
}
