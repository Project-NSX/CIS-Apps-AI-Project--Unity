using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wander : EnemyBaseFSM
{
    int currentWP;

    // State Machine Awake
    private void Awake()
    {

    }
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Call OnStateEnter from EnemyBaseFSM.
        // This will get the Agent info
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        // If agent has no destination, set one
        if (enemyAgent.destination == null)
        {
            currentWP = Random.Range(0, waypoints.Length);
        }      
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If distance between current waypoint and NPC is less than accuracy, Set new waypoint
        if (Vector3.Distance(waypoints[currentWP].transform.position,
                enemyAgent.transform.position) < waypointAccuracy)
        {
            currentWP = Random.Range(0, waypoints.Length);
            // If current waypoint is greater than or equal to the..
            // .. length of the array..
            if (currentWP >= waypoints.Length)
            {
                // Below will send the agent to a random waypoint
                currentWP = Random.Range(0, waypoints.Length);
            }
        }
        // Move agent to next destination
        enemyAgent.SetDestination(waypoints[currentWP].transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
