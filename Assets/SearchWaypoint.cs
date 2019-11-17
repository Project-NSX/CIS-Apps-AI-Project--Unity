using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SearchWaypoint : EnemyBaseFSM
{

    GameObject waypoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        waypoint = waypoints[0];

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (Vector3.Distance(waypoints[i].transform.position, player.transform.position) < Vector3.Distance(waypoint.transform.position, player.transform.position))
            {
                waypoint = waypoints[i];
            }


            Debug.Log("Current: " + waypoint);
        }

        Debug.Log("Current: " + waypoint);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAgent.destination = waypoint.transform.position;

        if (Vector3.Distance(waypoint.transform.position, enemyAgent.transform.position) < 1 || animator.GetBool("detectPlayer") == true)
        {
            animator.SetBool("searchWaypoint", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
