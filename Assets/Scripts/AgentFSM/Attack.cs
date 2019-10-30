using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyBaseFSM
{
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemyAgent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TODO add * rotationSpeed to the end of this and test this is how you're supposed to do it
        enemyAgent.transform.rotation = Quaternion.Slerp(enemyAgent.transform.rotation, Quaternion.LookRotation(player.transform.position - enemyAgent.transform.position), Time.deltaTime * 3);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyAgent.isStopped = false;
    }


}
