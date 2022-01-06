    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

public class QueueConsumerSpawner : MonoBehaviour
{
    public void Initialize(IConsumerSpawnService consumerSpawnService, List<Transform> waypoints)
    {
        _consumerSpawnService = consumerSpawnService;
        _waypoints = waypoints;

        _consumers = new List<Consumer>();
    }
    
    //Services
    private IConsumerSpawnService _consumerSpawnService;
    
    //Consumers
    private List<Consumer> _consumers;
    
    //Waypoints
    private List<Transform> _waypoints;


    public void UpdateConsumers(List<IConsumer> consumers)
    {
        _consumers = new List<Consumer>();
        for (int i = 0; i < consumers.Count; i++)
        {
            _consumers.Add((Consumer) consumers[i]);
        }
    }
    
    // ▶Bring Me The Horizon - Can You Feel My Heart
    public Consumer AddAverageConsumer()
    {
        var consumer = _consumerSpawnService.GetAverageConsumer();
        
        _consumers.Add(consumer);
        consumer.transform.position = _waypoints[_consumers.Count - 1].position;

        return consumer;
    }

    public void RemoveConsumer(IConsumer iconsumer)
    {
        var consumer = (Consumer) iconsumer;
        _consumers.Remove(consumer);
        if (!(iconsumer is PlayerConsumer))
        {
            consumer.gameObject.SetActive(false);
        }
    }
}
