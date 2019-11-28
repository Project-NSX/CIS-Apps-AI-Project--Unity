using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : EnemyBaseFSM
{
    // Timer, used to set a player detection bool (when the agent is retreating) back to false.
    float maxTime = 2;
    float timer = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If player is detected while retreating, wait for the timer amount then set to false.
        // This allows the agent time to turn and look at the player before setting the bool back to false
        if(animator.GetBool("detectPlayerRetreating")== true)
        {
            timer += Time.deltaTime;
            if (timer > maxTime)
            {
                animator.SetBool("detectPlayerRetreating", false);
                timer = 0.0f;
            }
        }

        // Set agent's destination to the player.
        enemyAgent.SetDestination(player.transform.position);
        // If player is not detected
        if (animator.GetBool("detectPlayer") == false)
        {
            // Search location bool to true - used to move to the last known position of the player
            animator.SetBool("searchLocation", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Just in case - set detected player (while retreating) bool to false
        animator.SetBool("detectPlayerRetreating", false);
    }   
}
