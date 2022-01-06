using System.Collections;
using UnityEngine;

public class ConsumerAnimator : MonoBehaviour
{
    public enum ConsumerAnimationState
    {
        Idle,
        Move,
        Bored
    }

    protected ConsumerAnimationState State
    {
        get
        {
            return _state;
        }
        set
        {
            if (_state != value)
            {
                _animator.SetTrigger(value.ToString());
            }
            
            _state = value;

        }
    }

    [SerializeField] protected ConsumerAnimationState _state;
    
    protected float _boredAnimationDelay = 5f;
    protected IEnumerator _boredCoroutine;
    
    //Components
    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        Idle();
    }

    public virtual void Move()
    {
        if (_boredCoroutine != null)
        {
            StopCoroutine(_boredCoroutine);
        }
        
        State = ConsumerAnimationState.Move;

    }

    public virtual void Idle()
    {
        if (_boredCoroutine != null)
        {
            StopCoroutine(_boredCoroutine);
        }
        
        State = ConsumerAnimationState.Idle;
        
        _boredCoroutine = WaitForBoredAnimation();
        StartCoroutine(_boredCoroutine);
    }

    protected virtual IEnumerator WaitForBoredAnimation()
    {
        yield return new WaitForSeconds(_boredAnimationDelay);
        State = ConsumerAnimationState.Bored;
        Idle();
    }
}
