using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : EnemyBaseFSM
{
    float maxTime = 3;
    float timer = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("detectPlayerRetreating")== true)
        {
            timer += Time.deltaTime;
            if (timer > maxTime)
            {
                animator.SetBool("detectPlayerRetreating", false);
                timer = 0.0f;
            }
        }

        enemyAgent.SetDestination(player.transform.position);
        if (animator.GetBool("detectPlayer") == false)
        {
            animator.SetBool("searchLocation", true);
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("detectPlayerRetreating", false);
    }
    
}
