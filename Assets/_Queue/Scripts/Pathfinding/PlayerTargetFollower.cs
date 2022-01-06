using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class PlayerTargetFollower : TargetFollower
{
    [Header("MidPoint")]
    [SerializeField] private Vector2 _midPointPosition;
    [SerializeField] private float _midPointReachDistance = 0.01f;
    public event Action MidPointReached;
    [SerializeField] private bool _wasMidPointReached;

    public void SetMidPointPosition(Vector2 position, Action callback)
    {
        _midPointPosition = position;
        
        _wasMidPointReached = false;
        MidPointReached += callback;
        
        OnStartedMove();
    }
    

    protected override void Move()
    {
        if (_ai == null)
            return;
        
        //_ai.destination = _targetPosition;

        if (!_wasMidPointReached)
        {
            _ai.destination = _midPointPosition;
            if (!_wasMidPointReached && Vector2.Distance(_transform.position, _midPointPosition) <= _midPointReachDistance)
            {
                OnMidPointReached();
            }
        }
        else
        {
            Debug.Log("Player target");
            _ai.destination = _targetPosition;
            if (!_wasTargetReached && Vector2.Distance(_transform.position, _targetPosition) <= _targetReachDistance)
            {
                OnTargetReached();
            }
        }


    }
    

    private void OnMidPointReached()
    {
        _wasMidPointReached = true;
        Debug.Log("Player midpoint reached");
        
        MidPointReached?.Invoke();
        MidPointReached = null;
        
        //OnEndedMove();
    }
}
