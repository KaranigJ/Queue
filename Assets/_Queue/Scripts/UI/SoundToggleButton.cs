using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SoundToggleButton : ToggleButton
{
    private ISoundService _soundService;
    
    [Inject]
    public void Construct(ISoundService soundService)
    {
        _soundService = soundService;
    }

    protected override void Awake()
    {
        State = _soundService.GetMuteState();
    }

    public override void OnPress()
    {
        base.OnPress();
        _soundService.ToggleMute();
    }
}
