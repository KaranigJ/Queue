using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class QueuePathfindingManager : MonoBehaviour
{
    [SerializeField] private List<TargetFollower> _queueFollowers;
    [SerializeField] private List<Transform> _queueWaypoints;

    public void Initialize(List<Transform> waypoints)
    {
        _queueWaypoints = waypoints;
    }

    public void UpdateQueueFollowers(List<IConsumer> consumers)
    {
        _queueFollowers = new List<TargetFollower>();
        
        for (int i = 0; i < consumers.Count; i++)
        {
            var follower = ((Consumer) consumers[i]).gameObject.GetComponent<TargetFollower>();
            _queueFollowers.Add(follower);
            follower.SetTargetPosition(_queueWaypoints[i].position);
        }
    }
}
