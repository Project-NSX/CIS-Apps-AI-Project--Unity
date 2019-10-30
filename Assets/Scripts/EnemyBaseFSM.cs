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

    // TODO - Add this to EnemyAI and pass it to here
    public float waypointAccuracy = 3.00f;

    // TODO: Add this to enemyAI and pass it to here
    public int rotationSpeed = 3;


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
