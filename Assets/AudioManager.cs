using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public static AudioManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        FindObjectOfType<OSTPlayer>().GetComponent<AudioSource>().volume = 0.2f;
    }

    public void play(AudioClip clip)
    {
        _audioSource.pitch = 1;
        _audioSource.PlayOneShot(clip);
    }

    public void play(AudioClip clip, float pitch)
    {
        var previousPitch = _audioSource.pitch;
        _audioSource.pitch = pitch;
        _audioSource.PlayOneShot(clip);
    }
}
