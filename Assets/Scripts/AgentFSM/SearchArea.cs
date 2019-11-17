using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : EnemyBaseFSM
{
    GameObject[] waypoints;
    GameObject waypoint;

    Vector3 playerLastPos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        playerLastPos = player.transform.position;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        enemyAgent.SetDestination(playerLastPos);
        //enemyAgent.destination = playerLastPos;

        if (Vector3.Distance(enemyAgent.transform.position, playerLastPos) < waypointAccuracy || animator.GetBool("detectPlayer") == true)
        {
            animator.SetBool("searchLocation", false);
        }

        // Keep agent more tightly on it's path
        if (enemyAgent.hasPath)
        {
            Vector3 toTarget = enemyAgent.steeringTarget - enemyAgent.transform.position;
            float turnAngle = Vector3.Angle(enemyAgent.transform.forward, toTarget);
            enemyAgent.acceleration = turnAngle * enemyAgent.speed;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        waypoint = waypoints[0];

        for (int i = waypoints.Length; i < waypoints.Length; i++)
        {
            if (Vector3.Distance(waypoints[i].transform.position, player.transform.position) < Vector3.Distance(waypoint.transform.position, player.transform.position))
            {
                waypoint = waypoints[i];
            }
        }
        enemyAgent.destination = waypoint.transform.position;
    }

}
