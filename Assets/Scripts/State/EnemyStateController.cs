using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private IEnemyState currentState;
    private Transform player;

    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float idleTime = 5f;
    public float walkTime = 10f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set the walk state as the default state
        TransitionToNextState(new EnemyWalkState(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate();
    }

    public void TransitionToNextState(IEnemyState nextState)
    {
        // If the current state is not empty
        if (currentState != null)
        {
            // Exit the current state
            currentState.OnStateExit();
        }

        // Fetch the next state into the current state
        currentState = nextState;

        // If the next state (now the current state) is not empty
        if (currentState != null)
        {
            // Enter that state
            currentState.OnStateEnter();
        }
    }

    public void SetNextDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public float GetChaseRange()
    {
        return chaseRange;
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
