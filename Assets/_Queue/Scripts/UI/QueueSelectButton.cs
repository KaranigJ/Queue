using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QueueSelectButton : MonoBehaviour
{
    [SerializeField] private int _queueIndex;
    
    //Services
    private IPlayerService _playerService;
    private ILevelStateService _levelStateService;
    private IEnvironmentService _environmentService;

    //Components
    private RaycastButton _raycastButton;
    private Transform _transform;

    [Inject]
    public void Construct(ILevelStateService levelStateService, IPlayerService playerService, IEnvironmentService environmentService)
    {
        _levelStateService = levelStateService;
        _playerService = playerService;
        _environmentService = environmentService;
    }
    
    private void Awake()
    {
        _raycastButton = GetComponent<RaycastButton>();
        _transform = transform;
    }

    private void Update()
    {
        if (_raycastButton.CheckIfClicked(_transform))
        {
            OnPress();
        }
    }

    private void OnPress()
    {
        Debug.Log($"{_queueIndex} Queue Chosen");
        
        _environmentService.SetOutline(false);
        
        _levelStateService.TryStartLevel();
        _playerService.ChangeQueue(_queueIndex);
    }
}
