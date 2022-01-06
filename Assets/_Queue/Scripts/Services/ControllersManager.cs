using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ControllersManager : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] _averageControllers;
    public static ControllersManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    public RuntimeAnimatorController GetRandomAverageController()
    {
        return _averageControllers[Random.Range(0, _averageControllers.Length)];
    }
}
