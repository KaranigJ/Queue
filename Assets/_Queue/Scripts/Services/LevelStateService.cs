using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum LevelState
{
    Init,
    Play,
    Win,
    Lose
}


public class LevelStateService : MonoBehaviour, ILevelStateService
{
    //Events
    public event Action LevelStarted;
    public event Action Won;
    public event Action Lost; 


    [SerializeField] private bool _wasLevelStarted;
    
    //States
    private LevelState State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            _levelUIService.SetScreen(value);
        }
    }
    [SerializeField] private LevelState _state;
    
    //Services
    private ILevelUIService _levelUIService;
    
    //Sub-Components
    private LevelTimer _levelTimer;

    [Inject]
    public void Construct(ILevelUIService levelUIService)
    {
        _levelUIService = levelUIService;
    }
    
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _levelTimer = new GameObject("LevelTimer").AddComponent<LevelTimer>();
        _levelTimer.Initialize(this, _levelUIService);
    }
    
    public void TryStartLevel()
    {
        if (State == LevelState.Init)
        {
            State = LevelState.Play;
            LevelStarted?.Invoke();
        }
    }

    
    public void OnTimeOver()
    {
        if (State == LevelState.Play)
        {
            State = LevelState.Lose;
            OnLose();
        }
    }

    public void OnPlayerServed()
    {
        if (State == LevelState.Play)
        {
            State = LevelState.Win;
            OnWin();
        }
    }

    private void OnLose()
    {
        Lost?.Invoke();
    }

    private void OnWin()
    {
        Won?.Invoke();
    }
    
}
