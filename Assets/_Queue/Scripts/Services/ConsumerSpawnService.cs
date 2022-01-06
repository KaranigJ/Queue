using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerSpawnService : MonoBehaviour, IConsumerSpawnService
{
    [SerializeField] private Consumer _averageConsumerPrefab;
    [SerializeField] private Consumer _grannyConsumerPrefab;
    [SerializeField] private Consumer _momConsumerPrefab;

    private const int StartPrefabCount = 5;
    
    private PoolMono<Consumer> _averageConsumerPool;
    private PoolMono<Consumer> _grannyConsumerPool;
    private PoolMono<Consumer> _momConsumerPool;

    private Transform _averageConsumerParent;
    private Transform _grannyConsumerParent;
    private Transform _momConsumerParent;

    /// <summary>
    /// Some shitcode. Will be fixed when it will be needed
    /// </summary>
    private void Awake()
    {
        _averageConsumerParent = new GameObject("AverageConsumers").transform;
        _grannyConsumerParent = new GameObject("GrannyConsumers").transform;
        _momConsumerParent = new GameObject("MomConsumers").transform;

        _averageConsumerPool = new PoolMono<Consumer>(_averageConsumerPrefab, StartPrefabCount, true, _averageConsumerParent);
        _grannyConsumerPool =
            new PoolMono<Consumer>(_grannyConsumerPrefab, StartPrefabCount, true, _grannyConsumerParent);
        _momConsumerPool = new PoolMono<Consumer>(_momConsumerPrefab, StartPrefabCount, true, _momConsumerParent);
    }

    public Consumer GetAverageConsumer()
    {
        return _averageConsumerPool.GetFreeElement(true);
    }

    public Consumer GetGrannyConsumer()
    {
        return _grannyConsumerPool.GetFreeElement(true);
    }

    public Consumer GetMomConsumer()
    {
        return _momConsumerPool.GetFreeElement(true);
    }
}
