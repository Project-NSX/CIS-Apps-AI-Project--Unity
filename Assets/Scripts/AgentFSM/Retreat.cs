using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Retreat : EnemyBaseFSM
{
    // Goal away from the player
    Vector3 newGoal;
    // Direction away from the player
    Vector3 fleeDirection;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // Call method to heal the agent while it's retreating
        Agent.GetComponent<EnemyAI>().InvokeRepeating("HealRetreat", 3.0f, 0.5f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get flee direction away from the player
        fleeDirection = (enemyAgent.transform.position - player.transform.position).normalized;
        // Make a goal based on the new direction
        newGoal = enemyAgent.transform.position + fleeDirection * 7;

        // Set destination to the goal away from the player
        enemyAgent.SetDestination(newGoal);

        // If player is close to the agent - agent is aware of the player
        if (animator.GetFloat("distanceToPlayer") < 15 )
        {
            animator.SetBool("detectPlayerRetreating", true);
        }
        else
        {
            // agent is not aware of the player
            animator.SetBool("detectPlayerRetreating", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop the agent from healing.
        Agent.GetComponent<EnemyAI>().CancelInvoke("HealRetreat");
    }
}
