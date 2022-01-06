using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    //In seconds
    private float _playTime = 45f;
    
    //Services
    private ILevelStateService _levelStateService;
    private ILevelUIService _levelUIService;

    private IEnumerator _waitCoroutine;

    public void Initialize(ILevelStateService levelStateService, ILevelUIService levelUIService)
    {
        _levelStateService = levelStateService;
        _levelUIService = levelUIService;

        _levelStateService.LevelStarted += Play;
        _levelStateService.Won += Stop;
    }
    
    private void Play()
    {
        _waitCoroutine = Wait();
        StartCoroutine(_waitCoroutine);
    }

    private void Stop()
    {
        StopCoroutine(_waitCoroutine);
    }

    private IEnumerator Wait()
    {
        float curTime = _playTime;

        while (curTime >= 0)
        {
            curTime -= Time.deltaTime;

            _levelUIService.SetTimerText(curTime);
            
            yield return null;
        }

        yield return new WaitForSeconds(1);
        
        _levelStateService.OnTimeOver();
    }
}
