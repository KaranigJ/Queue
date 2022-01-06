using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class DebugQueueManager : MonoBehaviour
{
#if UNITY_EDITOR
    private string[] _queueConsumers;
    [SerializeField] private Text _queuesText;
    [SerializeField] private GameObject _canvas;

    public static DebugQueueManager Instance;

    private void Awake()
    {
        Instance = this;
        _queueConsumers = new string[3];
    }

    public void SetQueueConsumers(string value, int index)
    {
        _queueConsumers[index] = value;
    }

    private void Update()
    {
        string s = $"Queue 0 \n {_queueConsumers[0]} Queue 1 \n {_queueConsumers[1]}  Queue 2 \n{_queueConsumers[2]}";
        _queuesText.text = s;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _canvas.SetActive(!_canvas.activeSelf);
        }
    }
#endif
}