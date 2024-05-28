using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : IEnemyState
{
    //// Declare a transform to store the Player game object
    //private Transform player;

    //// Declare a NavMesh agent
    //private NavMeshAgent agent;

    // Call the EnemyIdleState script
    private EnemyStateController anEnemyController;
    private float idleTimer;

    public EnemyIdleState(EnemyStateController enemyController)
    {
        this.anEnemyController = enemyController;
    }

    public void OnStateEnter()
    {
        idleTimer = 0f;
        anEnemyController.GetAnimator().SetTrigger("Idle");
    }

    public void OnStateUpdate()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= anEnemyController.idleTime)
        {
            if (PlayerInChaseRange())
            {
                anEnemyController.TransitionToNextState(new EnemyChaseState(anEnemyController));
            }
            else if (PlayerInAttackRange())
            {
                anEnemyController.TransitionToNextState(new EnemyAttackState(anEnemyController));
            }
            else
            {
                anEnemyController.TransitionToNextState(new EnemyWalkState(anEnemyController));
            }
        }
    }

    public void OnStateExit()
    {
        // Clean up or exit actions, if any
    }

    private bool PlayerInChaseRange()
    {
        return Vector3.Distance(anEnemyController.transform.position, anEnemyController.GetPlayerTransform().position) <= anEnemyController.GetChaseRange();
    }

    private bool PlayerInAttackRange()
    {
        return Vector3.Distance(anEnemyController.transform.position, anEnemyController.GetPlayerTransform().position) <= anEnemyController.GetAttackRange();
    }


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Fetcht the NavMeshAgent component on the enemy game object into the "agent" variable
    //    agent = animator.GetComponent<NavMeshAgent>();
    //    // Disable the NavMeshAgent component on the enemy
    //    agent.enabled = false;

    //    // set the timer to zero
    //    timer = 0;

    //    // Find and fetch the player game object into the "player" variable
    //    player = GameObject.FindGameObjectWithTag("Player").transform;

    //    chaseRange = anEnemyManager.chaseRange;
    //    attackRange = anEnemyManager.attackRange;
    //}

    //// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Start the timer
    //    timer += Time.deltaTime;

    //    // Transition to Walk state once timer reaches 10
    //    if (timer > 10)
    //    {
    //        animator.SetTrigger("walk");
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
    //}

    //// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
