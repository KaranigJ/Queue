
using UnityEngine;

public interface IQueueService
{
    public void Play();
    public void AppendConsumerToQueue(IConsumer consumer, int queueIndex);
    public void ChangePlayerQueue(IConsumer player, int currentQueueIndex, int targetQueueIndex);
    public void RemoveConsumerFromQueue(IConsumer consumer, int queueIndex);

    public Vector2 GetFirstFreeQueueWaypointPosition(int index);
    public Vector2 GetQueueMidWaypointPosition(int index);

    public void FillMinQueue();

}
