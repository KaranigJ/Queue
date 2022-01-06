using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum MusicState
{
    Play,
    TimerTick
}
public class MusicService : MonoBehaviour, IMusicService
{
    //State
    private MusicState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;

            switch (value)
            {
                case MusicState.Play:
                    _musicSource.volume = _playVolume;
                    _timerSource.mute = true;
                    
                    break;
                
                case MusicState.TimerTick:
                    _musicSource.volume = _timerTickVolume;
                    _timerSource.mute = false;
                    
                    break;
            }
        }
    }

    private MusicState _state;

    [Header("AudioClips")] 
    [SerializeField] private AudioClip _music;

    [SerializeField] private AudioClip _timerTick;
    
    [Header("Volumes")]
    [SerializeField] private float _timerTickVolume = 0.8f;
    private float _playVolume = 1f;

    [Header("AudioSources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _timerSource;
    
    //Services
    private ISoundService _soundService;

    [Inject]
    public void Construct(ISoundService soundService)
    {
        _soundService = soundService;

        _soundService.muteToggled += SetMute;
    }
    
    private void Awake()
    {
        _musicSource.clip = _music;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void SetMute(bool value)
    {
        _musicSource.mute = value;
    }

    public void SetState(MusicState state)
    {
        State = state;
    }

    public void PlayTick()
    {
        _timerSource.PlayOneShot(_timerTick);
    }
}
