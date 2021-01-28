using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SearchWaypoint : EnemyBaseFSM
{
    // waypoint to path to
    GameObject waypoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // Set waypoint to first waypoint in array (needs to be set before running)
        waypoint = waypoints[0];

        // For all waypoints in waypoint array...
        for (int i = 0; i < waypoints.Length; i++)
        {
            // if distance between waypoint and player is less than the distance between the waypoint and the player of the last waypoint
            if (Vector3.Distance(waypoints[i].transform.position, player.transform.position) < Vector3.Distance(waypoint.transform.position, player.transform.position))
            {
                // Set new waypoint to current waypoint
                waypoint = waypoints[i];
            }

            //Debug.Log("Current: " + waypoint);
        }
        // Shows waypoint closest to the player
        Debug.Log("Current: " + waypoint);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set destination to the waypoint
        enemyAgent.destination = waypoint.transform.position;

        // if agent is close to the waypoint or detects the player..
        if (Vector3.Distance(waypoint.transform.position, enemyAgent.transform.position) < 1 || animator.GetBool("detectPlayer") == true)
        {
            // Stop pathing to waypoint
            animator.SetBool("searchWaypoint", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
