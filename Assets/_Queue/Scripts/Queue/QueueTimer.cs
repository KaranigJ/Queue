using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Zenject;
using Random = UnityEngine.Random;

public class QueueTimer : MonoBehaviour
{
    
    
    //Services
    private ShopQueue _queue;

    public void Initialize(ShopQueue queue)
    {
        Debug.Log("Timer Init");
        _queue = queue;
    }

    public void StartTimer(IConsumer consumer, Action<IConsumer> callback = null)
    {
        
        StartCoroutine(WaitForTimer(consumer, callback));
    }

    private IEnumerator WaitForTimer(IConsumer iconsumer, Action<IConsumer> callback = null)
    {
        var consumer = (Consumer) iconsumer;
        Timings timings = _queue.IsPlayerIn() ? consumer.PlayerQueueTimings : consumer.OtherQueueTimings;
        var waitTime = Random.Range((float) timings.MinTime, (float) timings.MaxTime);

        Debug.Log(waitTime);
        
        yield return new WaitForSeconds(waitTime);
        
        callback?.Invoke(iconsumer);
    }
}
