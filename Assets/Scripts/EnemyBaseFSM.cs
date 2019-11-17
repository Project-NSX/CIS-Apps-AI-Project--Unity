using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseFSM : StateMachineBehaviour
{
    // Init Waypoints Array
    public GameObject[] waypoints;



    // Initialise NPC Gameobject
    public GameObject Agent;
    // Initialise agent
    public UnityEngine.AI.NavMeshAgent enemyAgent;
    // Start is called before the first frame update

    public GameObject player;


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

        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        // Check if there are any waypoints, if not, return
        if (waypoints.Length == 0) return;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // Keep agent more tightly on it's path
        if (enemyAgent.hasPath)
        {
            Vector3 toTarget = enemyAgent.steeringTarget - enemyAgent.transform.position;
            float turnAngle = Vector3.Angle(enemyAgent.transform.forward, toTarget);
            enemyAgent.acceleration = turnAngle * enemyAgent.speed;
        }
    }
}
