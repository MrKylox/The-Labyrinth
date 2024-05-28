using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : IEnemyState
{

    //// Declare a transform to store the Player game object
    //private Transform player;

    //// Declare a NavMesh agent
    //private NavMeshAgent agent;

    private EnemyStateController anEnemyController;


    // Constructor
    public EnemyChaseState(EnemyStateController enemyController)
    {
        this.anEnemyController = enemyController;
    }

    public void OnStateEnter()
    {
        // Trigger the animation to play
        anEnemyController.GetAnimator().SetTrigger("Chase");
    }

    public void OnStateUpdate()
    {
        if (PlayerInAttackRange())
        {
            anEnemyController.TransitionToNextState(new EnemyAttackState(anEnemyController));
        }
        else if (!PlayerInChaseRange())
        {
            // switch to idle state
            anEnemyController.TransitionToNextState(new EnemyIdleState(anEnemyController));
        }
        else
        {
            // chase the player
            anEnemyController.SetNextDestination(anEnemyController.GetPlayerTransform().position);
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
    //    chaseRange = anEnemyManager.chaseRange;
    //    attackRange = anEnemyManager.attackRange;

    //    // Fetcht the NavMeshAgent component on the enemy game object into the "agent" variable
    //    agent = animator.GetComponent<NavMeshAgent>();
    //    // Enable the NavMeshAgent component on the enemy
    //    agent.enabled = true;
    //    agent.speed = 1.5f;

    //    // Find and fetch the player game object into the "player" variable
    //    player = GameObject.FindGameObjectWithTag("Player").transform;

    //}

    //// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Let the enemy chase the plaer
    //    agent.SetDestination(player.transform.position);

    //    float distance = Vector3.Distance(player.position, animator.transform.position);

    //    // If the player is out of the chase effective range
    //    if (distance > chaseRange)
    //    {
    //        animator.SetTrigger("idle");

    //    }

    //    // If the player is within the attack effective range
    //    if (distance <= attackRange)
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
