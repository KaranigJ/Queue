using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueFiller : MonoBehaviour
{
    [SerializeField] private float _fillDelayTime = 4f;

    private IQueueService _queueService;

    public void Initialize(IQueueService queueService)
    {
        _queueService = queueService;
    }

    public void Play()
    {
        StartCoroutine(WaitForFillDelay());
    }

    private IEnumerator WaitForFillDelay()
    {
        yield return new WaitForSeconds(_fillDelayTime);
        
        _queueService.FillMinQueue();

        StartCoroutine(WaitForFillDelay());
    }
}
