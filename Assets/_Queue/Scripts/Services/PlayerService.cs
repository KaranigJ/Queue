using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class PlayerService : MonoBehaviour, IPlayerService
{
    [SerializeField] private int _targetQueueIndex;
    [SerializeField] private int _currentQueueIndex;

    [SerializeField] private PlayerConsumer _playerPrefab;

    [SerializeField] private Vector3 _playerStartPosition = new Vector3(-3.08f, 2, 0);

    private bool _wasQueueIntialized;
    
    //Player
    private IConsumer _playerIConsumer;
    private PlayerConsumer _playerConsumer;
    private PlayerTargetFollower _playerTargetFollower;
    
    //Services
    private IQueueService _queueService;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _targetQueueIndex = -1;
        _currentQueueIndex = -1;

        _playerConsumer = Instantiate(_playerPrefab, _playerStartPosition, Quaternion.identity);
        _playerIConsumer = _playerConsumer.GetComponent<IConsumer>();
        _playerTargetFollower = _playerConsumer.GetComponent<PlayerTargetFollower>();
        _playerConsumer.gameObject.SetActive(false);
    }

    [Inject]
    public void Construct(IQueueService queueService)
    {
        _queueService = queueService;
    }


    public void ChangeQueue(int index)
    {
        if (!_wasQueueIntialized)
        {
            _wasQueueIntialized = true;
            _playerConsumer.gameObject.SetActive(true);
        }
        TargetQueue(index);
    }

    
    private void TargetQueue(int index)
    {
        if (_currentQueueIndex != -1)
        {
            _queueService.RemoveConsumerFromQueue(_playerIConsumer, _currentQueueIndex);
        }
        
        _targetQueueIndex = index;
        _currentQueueIndex = -1;
        
        _playerTargetFollower.SetMidPointPosition(_queueService.GetQueueMidWaypointPosition(_targetQueueIndex), () =>
        {
            _playerTargetFollower.SetTargetPosition(_queueService.GetFirstFreeQueueWaypointPosition(_targetQueueIndex), OnTargetQueueReached);
        });
    }

    public void OnTargetQueueReached()
    {
        _queueService.AppendConsumerToQueue(_playerIConsumer, _targetQueueIndex);
        //_queueService.ChangePlayerQueue(_playerIConsumer, _currentQueueIndex, _targetQueueIndex);
        _currentQueueIndex = _targetQueueIndex;
        _targetQueueIndex = -1;
    }
}
