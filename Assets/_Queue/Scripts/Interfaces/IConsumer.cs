using System;
using UnityEngine;

public interface IConsumer
{
    public event Action<IConsumer> Served;

    public void SetTargetPosition(Vector2 position);
}
