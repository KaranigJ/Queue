using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.RVO;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class QueueService : MonoBehaviour, IQueueService
{
    //Waypoints
    [SerializeField] private QueueWaypointer[] _waypointers;
    [SerializeField] private List<Transform> _spawnPoints;

    [SerializeField] private ShopQueue[] _queues;
    private int QueuesCount = 3;
    
    //Services
    private IConsumerSpawnService _consumerSpawnService;
    private ILevelStateService _levelStateService;
    
    //Components
    private QueueFiller _queueFiller;

    [Inject]
    public void Construct(IConsumerSpawnService consumerSpawnService, ILevelStateService levelStateService)
    {
        _consumerSpawnService = consumerSpawnService;
        _levelStateService = levelStateService;

        _levelStateService.LevelStarted += Play;
    }
    
    private void Awake()
    {
        Initialize();
    }
    
    public void Initialize()
    {
        _queueFiller = new GameObject("Queue Filler").AddComponent<QueueFiller>();
        _queueFiller.Initialize(this);

        _queues = new ShopQueue[QueuesCount];

        for (int i = 0; i < _queues.Length; i++)
        {
            var queue = new GameObject($"Queue {i}").AddComponent<ShopQueue>();
            queue.Initialize(this, _consumerSpawnService, _levelStateService, _waypointers[i], i);
            _queues[i] = queue;
        }
    }
    
    public void Play()
    {
        _queueFiller.Play();
        foreach (var queue in _queues)
        {
            queue.Play();
        }
    }

    public void ChangePlayerQueue(IConsumer player, int currentQueueIndex, int targetQueueIndex)
    {
        RemoveConsumerFromQueue(player, currentQueueIndex);
        AppendConsumerToQueue(player, targetQueueIndex);
    }
    public void AppendConsumerToQueue(IConsumer consumer, int queueIndex)
    {
        _queues[queueIndex].AppendConsumer(consumer);
    }

    public void RemoveConsumerFromQueue(IConsumer consumer, int queueIndex)
    {
        _queues[queueIndex].RemoveConsumer(consumer);
    }

    #region Waypoints

    /// <summary>
    /// Returns first free waypoint (after last taken)
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Vector2 GetFirstFreeQueueWaypointPosition(int index)
    {
        return _queues[index].GetFirstFreeWaypointPosition();
    }

    public Vector2 GetQueueMidWaypointPosition(int index)
    {
        return _queues[index].GetMidWaypointPosition();
    }

    #endregion

    private Vector2 GetRandomSpawnpointPosition()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
    }

    public void FillMinQueue()
    {
        //Find min Queue index
        var queuesCapacities = new List<int>();
        var minQueueIndex = 0;
        var minConsumerCount = int.MaxValue;

        for (int i = 0; i < _queues.Length; i++)
        {
            var consumerCount = _queues[i].GetConsumerCount();

            if (consumerCount < minConsumerCount)
            {
                minConsumerCount = consumerCount;
                minQueueIndex = i;
            }
        }

        if (_queues[minQueueIndex].IsFull())
        {
            minQueueIndex = -1;
            
            for (int i = 0; i < _queues.Length; i++)
            {
                if (!_queues[i].IsFull())
                {
                    minQueueIndex = i;
                    break;
                }
            }
        }

        if (minQueueIndex != -1)
        {
            var consumer = (NPConsumer) _queues[minQueueIndex].CreateConsumer();
            consumer.transform.position = GetRandomSpawnpointPosition();

            consumer.TargetFollower.SetTargetPosition(_queues[minQueueIndex].GetLastWaypointPosition(),
                () => { _queues[minQueueIndex].AppendConsumer((IConsumer) consumer); });
        }
    }
}
