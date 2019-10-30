using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseFSM : StateMachineBehaviour
{
    // Initialise NPC Gameobject
    public GameObject Agent;
    // Initialise agent
    public UnityEngine.AI.NavMeshAgent enemyAgent;
    // Start is called before the first frame update

    public GameObject player;


    public float waypointAccuracy = 3.00f;


    public override void OnStateEnter(
       Animator animator,
       AnimatorStateInfo stateInfo,
       int layerIndex)
    {
        // Get NPC from animator
        Agent = animator.gameObject;

        // Set Agent
        enemyAgent = Agent.GetComponent<UnityEngine.AI.NavMeshAgent>();

        player = Agent.GetComponent<EnemyAI>().GetPlayer();


    }
}
