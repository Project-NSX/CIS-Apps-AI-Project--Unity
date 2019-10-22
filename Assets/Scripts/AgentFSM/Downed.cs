using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Downed : EnemyBaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        

        // FIXME:
        // This Invoke Repeating does not work.
        // Cancelling the invoke in Retreat causes this invoke to not work
        Agent.GetComponent<EnemyAI>().InvokeRepeating("HealDowned", 1f, 0.5f);
        Debug.Log("WTF IS HAPPENING");

        enemyAgent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent.GetComponent<EnemyAI>().CancelInvoke("HealDowned");
        enemyAgent.isStopped = false;
    }
}
