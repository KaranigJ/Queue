using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] protected Image _image;
    
    [Header("Sprites")]
    [SerializeField] protected Sprite _spriteFalse;
    [SerializeField] protected Sprite _spriteTrue;

    protected bool State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;
            _image.sprite = value ? _spriteFalse : _spriteTrue;
        }
    }
    protected bool _state;

    protected virtual void Awake()
    {
        State = false;
    }

    public virtual void OnPress()
    {
        State = !State;
    }
}
