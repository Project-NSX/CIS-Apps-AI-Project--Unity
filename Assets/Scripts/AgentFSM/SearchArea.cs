using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : EnemyBaseFSM
{
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
        
        //enemyAgent.SetDestination(playerLastPos);
        enemyAgent.destination = playerLastPos;

        if (Vector3.Distance(enemyAgent.transform.position, playerLastPos) < waypointAccuracy || animator.GetBool("detectPlayer") == true)
        {
            animator.SetBool("searchLocation", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
