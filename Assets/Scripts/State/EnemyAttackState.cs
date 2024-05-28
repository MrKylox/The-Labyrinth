using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : IEnemyState
{
    //// Declare a transform to store the Player game object
    //private Transform player;

    //// Declare a NavMesh agent
    //private NavMeshAgent agent;

    // Call the EnemyIdleState script
    private EnemyStateController anEnemyController;


    public EnemyAttackState(EnemyStateController enemyController)
    {
        this.anEnemyController = enemyController;
    }

    public void OnStateEnter()
    {
        anEnemyController.GetAnimator().SetTrigger("Attack");
    }

    public void OnStateUpdate()
    {
        if (!PlayerInAttackRange())
        {
            anEnemyController.TransitionToNextState(new EnemyChaseState(anEnemyController));
        }
    }

    public void OnStateExit()
    {
        // Clean up or exit actions, if any
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

    //    // Find and fetch the player game object into the "player" variable
    //    player = GameObject.FindGameObjectWithTag("Player").transform;

    //}

    //// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Let the enemy prefab face the player
    //    animator.transform.LookAt(player);
    //    float distance = Vector3.Distance(player.position, animator.transform.position);

    //    // If the player is out of the attack effective range
    //    if (distance > attackRange)
    //    {
    //        animator.SetTrigger("chase");

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
