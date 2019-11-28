using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : EnemyBaseFSM
{
    // Player's last known position
    Vector3 playerLastPos;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // Set player's last known position to current position.
        playerLastPos = player.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set destination to player's last known position
        enemyAgent.SetDestination(playerLastPos);

        // If the agent is close to the player's last known position, or the player is detected
        if (Vector3.Distance(enemyAgent.transform.position, playerLastPos) < waypointAccuracy || animator.GetBool("detectPlayer") == true)
        {
            // Do not search the player's last known location
            animator.SetBool("searchLocation", false);
        }
        
        // If the agent is close to the player and the agent does now know where the player is
        if (Vector3.Distance(enemyAgent.transform.position, playerLastPos) < waypointAccuracy && animator.GetBool("detectPlayer") == false)
        {
            // Search the waypoint closest to the player.
            animator.SetBool("searchWaypoint", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
