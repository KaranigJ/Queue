using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Consumer : MonoBehaviour, IConsumer
{
    public event Action<IConsumer> Served;
    
    [Header("Timings")]
    public Timings PlayerQueueTimings;
    
    //Timings of Queue without Player
    public Timings OtherQueueTimings;
    
    //Components
    public SpriteRenderer SpriteRenderer;
    public TargetFollower TargetFollower;
    [SerializeField] private ConsumerAnimator _animator;

    protected virtual void Awake()
    {
        TargetFollower.StartedMove += _animator.Move;
        TargetFollower.EndedMove += _animator.Idle;
    }

    public void SetTargetPosition(Vector2 position)
    {
        throw new NotImplementedException();
    }
}
