using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;
using Random = UnityEngine.Random;

[System.Serializable]
public class  ShopQueue : MonoBehaviour
{
    private int _queueIndex;

    //Waypoints
    private List<Transform> _waypoints;

    //Consumers
    [SerializeField] private int _startConsumerAmount = 7;
    private int _consumerCapacity;
    private int _minConsumerAmount = 5;
    private int _maxConsumerAmount = 9;
    
    private List<IConsumer> _consumers;
    

    //Services
    private IQueueService _queueService;
    private IConsumerSpawnService _consumerSpawnService;
    private ILevelStateService _levelStateService;
    
    //Sub-Components
    private QueueTimer _timer;
    private QueueConsumerSpawner _queueConsumerSpawner;
    private QueuePathfindingManager _queuePathfindingManager;
    private QueueSpriteManager _queueSpriteManager;
    private QueueWaypointer _queueWaypointer;


    public void Initialize(IQueueService queueService, IConsumerSpawnService consumerSpawnService,
        ILevelStateService levelStateService,
        QueueWaypointer queueWaypointer, int queueIndex)
    {
        _queueIndex = queueIndex;
        _waypoints = queueWaypointer.Waypoints;
        _consumerCapacity = _waypoints.Count;
        
        //Services
        _levelStateService = levelStateService;
        _queueService = queueService;
        _consumerSpawnService = consumerSpawnService;

        //Sub-Components
        _queueWaypointer = queueWaypointer;

        _timer = new GameObject("Timer").AddComponent<QueueTimer>();
        _timer.Initialize(this);
        _timer.transform.parent = this.transform;

        //Spawner
        _queueConsumerSpawner = new GameObject("Spawner").AddComponent<QueueConsumerSpawner>();
        _queueConsumerSpawner.Initialize(_consumerSpawnService, _waypoints);
        _queueConsumerSpawner.transform.parent = this.transform;

        //Pathfinding
        _queuePathfindingManager = new GameObject("Pathfinder").AddComponent<QueuePathfindingManager>();
        _queuePathfindingManager.Initialize(_waypoints);
        _queuePathfindingManager.transform.parent = this.transform;

        //Sprite
        _queueSpriteManager = new GameObject("SpriteManager").AddComponent<QueueSpriteManager>();
        _queueSpriteManager.transform.parent = this.transform;

        //Local
        _consumerCapacity = _startConsumerAmount; /*Random.Range(_minConsumerAmount, _maxConsumerAmount);*/

        _consumers = new List<IConsumer>();
        for (int i = 0; i < _consumerCapacity; i++)
        {
            _consumers.Add(CreateConsumer());
        }

        _queuePathfindingManager.UpdateQueueFollowers(_consumers);
    }

    public void Play()
    {
        Debug.Log("Queue Play");
        ServeNextConsumer();
    }

    #region Queue Manipulations

    private void ServeNextConsumer()
    {
        if (_consumers.Count > 0)
        {
            _timer.StartTimer(_consumers[0], OnConsumerServed);
        }

    }
    
    private void UpdateQueueGraphics()
    {
        _queueConsumerSpawner.UpdateConsumers(_consumers);
        _queuePathfindingManager.UpdateQueueFollowers(_consumers);
        _queueSpriteManager.UpdateSprites(_consumers);
    }

    #endregion
    
    #region  Queue Data

    public bool IsPlayerIn()
    {
        foreach (var consumer in _consumers)
        {
            if (consumer is PlayerConsumer)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsFull()
    {
        return (_consumers.Count == _consumerCapacity);
    }

    public int GetConsumerCount()
    {
        return _consumers.Count;
    }
    

    
    #endregion

    #region Waypoints

    public Vector2 GetLastWaypointPosition()
    {
        return _waypoints[_waypoints.Count - 1].position;
    }

    /// <summary>
    /// Waypoint before last taken waypoint
    /// </summary>
    /// <returns></returns>
    public Vector2 GetFirstFreeWaypointPosition()
    {
        return _waypoints[_consumers.Count].position;
    }

    public Vector2 GetMidWaypointPosition()
    {
        return _queueWaypointer.MidWaypoint.position;
    }

    #endregion

    #region Consumer Manipulations
    
    private void OnConsumerServed(IConsumer consumer)
    {
        //Player Served
        if (consumer is PlayerConsumer)
        {
            _levelStateService.OnPlayerServed();
            
            return;
        }
        
        RemoveConsumer(consumer);
    }
    public Consumer CreateConsumer()
    {
        var consumer = _queueConsumerSpawner.AddAverageConsumer();
        
        return consumer;
    }

    public void AppendConsumer(IConsumer consumer)
    {
        _consumers.Add(consumer);
        consumer.Served += OnConsumerServed;
        
        UpdateQueueGraphics();
    }

    public void CreateAndAppendConsumer()
    {
        AppendConsumer((IConsumer) CreateConsumer());
    }
    
    public void RemoveConsumer(IConsumer consumer)
    {
        Debug.Log("Consumer Removed");
        if (_consumers.Contains(consumer))
        {
            _consumers.Remove(consumer);
            _queueConsumerSpawner.RemoveConsumer(consumer);
            consumer.Served -= OnConsumerServed;
        
            UpdateQueueGraphics();
        
            ServeNextConsumer();
        }
    }
    
    public void InsertConsumer(IConsumer consumer, int index)
    {
        _consumers.Insert(index, consumer);
        consumer.Served += OnConsumerServed;
    }

    #endregion

    #region Debug
    #if UNITY_EDITOR
    public string GetConsumersForDebug()
    {
        var debugConsumers = new List<Consumer>();
        string s = "";
            
        for (int i = 0; i < _consumers.Count; i++)
        {
            debugConsumers.Add((Consumer)_consumers[i]);

            s += debugConsumers[i].gameObject.name + "\n";
        }

        return s;
    }

    private void Update()
    {
        DebugQueueManager.Instance.SetQueueConsumers(GetConsumersForDebug(), _queueIndex); 
    }
    #endif
    #endregion

    
}
