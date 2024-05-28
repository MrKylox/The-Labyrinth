using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    private EnemyManager enemyManager;
    private NavMeshAgent agent;
    private bool reachedDestination = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        if (reachedDestination)
        {
            SetRandomDestination();
        }
        else
        {
            MoveToDestination();
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomPoint = enemyManager.GetRandomPosition(); // Adjust the radius as needed
        agent.SetDestination(randomPoint);
        reachedDestination = false;
    }

    void MoveToDestination()
    {
        // Check if the agent has reached its destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            reachedDestination = true;
        }
    }

}
