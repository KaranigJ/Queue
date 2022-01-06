using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIService : MonoBehaviour, ILevelUIService
{
    [Header("Text")]
    [SerializeField] private TimerTextDisplay _timerText;

    [Header("Screens")]
    [SerializeField] private GameObject _initScreen;
    [SerializeField] private GameObject _playScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    private void Awake()
    {
        SetScreen(LevelState.Init);
    }

    public void SetTimerText(float time)
    {
        _timerText.SetText(Mathf.RoundToInt(time));
    }

    //Hard-coded to save time
    public void SetScreen(LevelState levelState)
    {
        _initScreen.SetActive(false);
        _playScreen.SetActive(false);
        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);
        
        switch (levelState)
        {
            case LevelState.Init:
                _initScreen.SetActive(true);
                break;
            
            case LevelState.Play:
                _playScreen.SetActive(true);
                break;
            
            case LevelState.Win:
                _winScreen.SetActive(true);
                break;
            
            case LevelState.Lose:
                _loseScreen.SetActive(true);
                break;
        }
    }
}
