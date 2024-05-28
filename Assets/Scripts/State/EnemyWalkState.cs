using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkState : IEnemyState
{
    // Declare a timer to record the time that the enemy has been in Idle state
    private float timer;

    //// Declare a transform to store the Player game object
    //private Transform player;

    //// Declare a NavMesh agent
    //private NavMeshAgent agent;

    private EnemyStateController anEnemyController;
    private EnemyManager anEnemyManager;
    private Vector3 nextDestination;
    private float walkTimer;



    // Constructor
    public EnemyWalkState(EnemyStateController enemyController)
    {
        this.anEnemyController = enemyController;
    }

    public void OnStateEnter()
    {
        SetRandomWalkDestination();
        walkTimer = 0f;
    }

    public void OnStateUpdate()
    {
        walkTimer += Time.deltaTime;

        if (walkTimer >= anEnemyController.walkTime)
        {
            anEnemyController.TransitionToNextState(new EnemyIdleState(anEnemyController));
        }

        if (PlayerInChaseRange())
        {
            anEnemyController.TransitionToNextState(new EnemyChaseState(anEnemyController));
        }

        if (ReachedDestination())
        {
            SetRandomWalkDestination();
        }

    }

    public void OnStateExit()
    {
        // Clean up or exit actions, if any
    }

    private void SetRandomWalkDestination()
    {
        nextDestination = anEnemyManager.GetRandomPosition();
        anEnemyController.SetNextDestination(nextDestination);
    }

    private bool ReachedDestination()
    {
        return Vector3.Distance(anEnemyController.transform.position, nextDestination) < 1f;
    }

    private bool PlayerInChaseRange()
    {
        return Vector3.Distance(anEnemyController.transform.position, anEnemyController.GetPlayerTransform().position) <= anEnemyController.GetChaseRange();
    }



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    chaseRange = anEnemyManager.chaseRange;
    //    attackRange = anEnemyManager.attackRange;
    //    nextDestination = anEnemyManager.GetRandomPosition();

    //    // Fetcht the NavMeshAgent component on the enemy game object into the "agent" variable
    //    agent = animator.GetComponent<NavMeshAgent>();
    //    // Enable the NavMeshAgent component on the enemy
    //    agent.enabled = true;
    //    agent.SetDestination(nextDestination);

    //    // set the timer to zero
    //    timer = 0;

    //    // Find and fetch the player game object into the "player" variable
    //    player = GameObject.FindGameObjectWithTag("Player").transform;

    //}        


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Start the timer
    //    timer += Time.deltaTime;
    //    // Transition to Idle state once timer reaches 30
    //    if (timer > 30)
    //    {
    //        animator.SetTrigger("idle");
    //    }

    //    // Calculate the distance between the enemy and the player
    //    float distance = Vector3.Distance(player.position, animator.transform.position);

    //    // Transition to Chase state once player is within chase effective range
    //    if (distance > attackRange && distance <= chaseRange)
    //    {
    //        animator.SetTrigger("chase");

    //    }

    //    // Transition to Attack state once player is within attack effective range
    //    if (distance <= chaseRange)
    //    {
    //        animator.SetTrigger("attack");
    //    }

    //    if (agent.remainingDistance < agent.stoppingDistance) 
    //    {
    //        // Get a new destination
    //        nextDestination = anEnemyManager.GetRandomPosition();
    //        agent.SetDestination(nextDestination);
    //    }



    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}


}
