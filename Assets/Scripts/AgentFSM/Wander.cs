using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : EnemyBaseFSM
{
    // Init Waypoints Array
    GameObject[] waypoints;


    // Variable to track current waypoint
    public int currentWP;



    // State Machine Awake
    private void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");

    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Call OnStateEnter from EnemyBaseFSM.
        // This will get the Agent info
        base.OnStateEnter(animator, stateInfo, layerIndex);
        // Go to starting waypoing
        currentWP = 0;
        // Below will send the agent to a random waypoint
        // currentWP = Random.Range(0, waypoints.Length);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if there are any waypoints, if not, return
        if (waypoints.Length == 0) return;

        // If distance between current waypoint and NPC is less than accuracy.
        if (Vector3.Distance(waypoints[currentWP].transform.position,
            Agent.transform.position) < waypointAccuracy)
        {


            currentWP = Random.Range(0, waypoints.Length);
            // If current waypoint is greater than or equal to the..
            // .. length of the array..
            if (currentWP >= waypoints.Length)
            {
                // Go back to the start
                // currentWP = 0;
                // Below will send the agent to a random waypoint
                currentWP = Random.Range(0, waypoints.Length);
            }


        }
        // Move agent to next destination
        enemyAgent.SetDestination(waypoints[currentWP].transform.position);

        // Keep agent more tightly on it's path
        if (enemyAgent.hasPath)
        {
            Vector3 toTarget = enemyAgent.steeringTarget - enemyAgent.transform.position;
            float turnAngle = Vector3.Angle(enemyAgent.transform.forward, toTarget);
            enemyAgent.acceleration = turnAngle * enemyAgent.speed;
        }
        // If agent does not have path, pick a new waypoint. 
        else
        {
            currentWP = Random.Range(0, waypoints.Length);
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
