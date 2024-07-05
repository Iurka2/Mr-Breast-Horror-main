using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rat : MonoBehaviour {

    NavMeshAgent rat;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    private void Start ( ) {
        rat = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    private void Update ( ) {
        if (Vector3.Distance(transform.position, target) < 1 )
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination ( ) {
        target = waypoints[waypointIndex].position;
        rat.SetDestination(target);
    }

    void IterateWaypointIndex ( ) {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

}
