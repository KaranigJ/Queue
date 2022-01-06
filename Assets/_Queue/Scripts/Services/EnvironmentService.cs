using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentService : MonoBehaviour, IEnvironmentService
{
    [SerializeField] private SpriteRenderer[] _cashboxes;

    [Header("Materials")]
    [SerializeField] private Material _defaultMaterial;

    [SerializeField] private Material _outlineMaterial;

    private void Awake()
    {
        SetOutline(true);
    }

    public void SetOutline(bool state)
    {
        foreach (var cashbox in _cashboxes)
        {
            cashbox.material = _outlineMaterial;
        }
    }
}
