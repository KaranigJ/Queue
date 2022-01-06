using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundService : MonoBehaviour, ISoundService
{
    [SerializeField] private bool _mute;
    
    public event Action<bool> muteToggled;

    private void Awake()
    {
        _mute = false;
    }

    public void ToggleMute()
    {
        _mute = !_mute;
        muteToggled?.Invoke(_mute);
    }

    public bool GetMuteState()
    {
        return _mute;
    }
}
