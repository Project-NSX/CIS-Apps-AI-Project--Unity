using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Initialise animator
    Animator anim;

    // Game object used to store the player
    GameObject player;
    
    // Head of AI, used to cast the way - Set in the inspector
    public GameObject head;

    // Health of the Agent
    int health = 100;

    // Vision variables
    // Range the agent can see - Set in the inspector
    public float visionRange;

    // Ray from the agent to the player
    RaycastHit hit;
    
    // Player's position - stored when Agent loses sight of the player
    Vector3 playerLastPos;

    // Vision range of the agent - set in the inspector
    public float visionAngle;
    // Hearing distance of the agent - set in the inspector
    public float hearingDistance;

    // Speed the player must be moving for the agent to hear them.
    public float hearMovementSpeed;

    // Vector 3's for player position and velocity of the player
    Vector3 playerPos, playerVelocity;

    // Initialise nav mesh agent
    private NavMeshAgent agent;
    // Speed of the agent when walking normally
    public float speed = 3.5f;
    // Speed of the agent when walking through mud
    public float speedMud = 1.5f;

    // Method called from NPCBaseFSM
    public GameObject GetPlayer()
    {
        // Return the gameobject of the player (Set below via tag)
        return player;
    }


    // Use this for initialization
    void Start()
    {
        // Get animator
        anim = GetComponent<Animator>();

        // Get agent
        agent = GetComponent<NavMeshAgent>();

        // Get player using their tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Current player position - used to calculate player velocity
        playerPos = player.transform.position;

        // Update health amount from the animator, run every 1 second.
        InvokeRepeating("UpdateHealth", 0.1f, 0.1f);
    }

    private void Update()
    {
        // Player's velocity
        playerVelocity = (player.transform.position - playerPos) / Time.deltaTime;
        // Player's current position - updated at all times
        playerPos = player.transform.position;
        // Show player's speed in the debug console
        //Debug.Log("Player Speed: " + playerVelocity.magnitude);
    }

    private void LateUpdate()
    {
        // VISION CODE - Most complicated code

        // Vector from the agent's head to the player
        Vector3 distanceToPlayer = player.transform.position - head.transform.position;

        // Set the distanceToPlayer parameter
        anim.SetFloat("distanceToPlayer",
                      Vector3.Distance(transform.position,
                      player.transform.position));
        //Calculate Angle between direction vector above and the..
        //..forward facing vector of the NPC
        float angle = Vector3.Angle(distanceToPlayer, head.transform.forward);

        // Debug ray to see where the distance vector is being projected
        //Debug.DrawRay(head.transform.position, distanceToPlayer, Color.red);

        // Project Ray from the head of the agent to the player
        if (Physics.Raycast(head.transform.position, distanceToPlayer, out hit))
        {
            // If ray hits the player & is within vision range, or the player is within hearing distance and moving above the hearSpeed, or the agent has finished retreating and knows where the player is
            if (hit.collider.gameObject.tag == "Player" && distanceToPlayer.magnitude < visionRange && angle < visionAngle || distanceToPlayer.magnitude < hearingDistance && playerVelocity.magnitude > hearMovementSpeed || anim.GetBool("detectPlayerRetreating") == true)
            {
                // Set detect player bool to true in animator
                anim.SetBool("detectPlayer", true);
            }
            else
            {
                // Set detect player bool to false in the ainmator
                anim.SetBool("detectPlayer", false);
            }
        }

        // Take health from the enemy on space press
        if (Input.GetKeyDown("space"))
        {
            health -= 10;
        }
        // Set health of the agent
        anim.SetInteger("health", health);
        
        // Find nearest point on mud.
        int mudMask = 1 << NavMesh.GetAreaFromName("Mud");
        // Hit for navmesh
        NavMeshHit navHit;
        // Position of the agent
        agent.SamplePathPosition(-1, 0.0f, out navHit);
        // if Nav mesh where the agent currently is is called "Mud"
        if (navHit.mask == mudMask)
        {
            // In Mud
            // Slow agent's speed
            agent.speed = speedMud;
            //Debug.Log("Agent in The Mud");
        }
        else
        {
            // Agent's normal speed
            agent.speed = speed;   
        }
    }

    // Method to update the health of the agent
    // Ensures the agent's health is 0-100, and does not go over or under.
    public void UpdateHealth()
    {
        if (health < 0)
        {
            health = 0;
        }
        else if (health > 100)
        {
            health = 100;
        }
    }

    // Heal the agent when the agent is retreating
    public void HealRetreat()
    {
        health += 5;
    }

    // Heal the agent when the agent is downed - used seperate method to prevent a bug
    // Bug- When using 1 method for both heals, the method would not start running on the 
    // next on state enter.
    public void HealDowned()
    {
        health += 5;
    }
}
