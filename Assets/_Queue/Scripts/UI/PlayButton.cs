using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayButton : MonoBehaviour
{   

    private IQueueService _queueService;

    [Inject]
    public void Construct(IQueueService queueService)
    {
        _queueService = queueService;
    }
    
    public void OnPress()
    {
        _queueService.Play();
    }
}
