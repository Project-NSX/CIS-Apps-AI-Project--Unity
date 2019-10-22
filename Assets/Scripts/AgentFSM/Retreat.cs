using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Retreat : EnemyBaseFSM
{
    Vector3 newGoal;
    Vector3 fleeDirection;
    NavMeshPath newPath;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Agent.GetComponent<EnemyAI>().InvokeRepeating("HealRetreat", 3.0f, 0.5f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fleeDirection = (enemyAgent.transform.position - player.transform.position).normalized;
        newGoal = enemyAgent.transform.position + fleeDirection * 7;

        enemyAgent.SetDestination(newGoal);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Agent.GetComponent<EnemyAI>().CancelInvoke("HealRetreat");
    }

}
