using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnimatorSetup : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private ControllersManager _controllersManager;

    private void Start()
    {
        _controllersManager = ControllersManager.Instance;
        _animator.runtimeAnimatorController = _controllersManager.GetRandomAverageController();
        
        Debug.Log($"Controller {ControllersManager.Instance == null}");
    }

}
