using UnityEngine;
using System.Collections;
using System;

namespace Pathfinding
{
    public class TargetFollower : VersionedMonoBehaviour
    {
        //Constant events
        public event Action StartedMove;
        public event Action EndedMove;
        
        [Header("Target")]
        [SerializeField] protected float _targetReachDistance = 0.8f;
        [SerializeField] protected Vector2 _targetPosition;
        public event Action TargetReached;
        [SerializeField] protected bool _wasTargetReached;
        
        //Components
        protected IAstarAI _ai;
        protected Transform _transform;

        protected virtual void Awake()
        {
            _transform = transform;
        }
        
        protected virtual void OnEnable()
        {
            _ai = GetComponent<IAstarAI>();

            if (_ai == null)
                return;

            _ai.onSearchPath += Update;
        }


        protected virtual void OnDisable()
        {
            if (_ai == null)
                return;

            _ai.onSearchPath -= Update;
        }

        public virtual void SetTargetPosition(Vector2 position, Action callback = null)
        {
            _targetPosition = position;
            
            _wasTargetReached = false;
            TargetReached = callback;
            
            OnStartedMove();
        }
        
        protected virtual void OnTargetReached()
        {
            _wasTargetReached = true;
            Debug.Log("Player target reached");

            TargetReached?.Invoke();
            TargetReached = null;
            
            OnEndedMove();
        }

        #region Events

        protected virtual void OnEndedMove()
        {
            EndedMove?.Invoke();
        }

        protected virtual void OnStartedMove()
        {
            StartedMove?.Invoke();
        }

        #endregion
        
        protected virtual void Update()
        {
            Move();
        }

        
        protected virtual void Move()
        {
            if (_ai == null)
                return;
            
            _ai.destination = _targetPosition;
            
            if (!_wasTargetReached && Vector2.Distance(_transform.position, _targetPosition) <= _targetReachDistance)
            {
                OnTargetReached();
            }
        }
    }
}