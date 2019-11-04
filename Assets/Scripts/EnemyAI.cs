using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Initialise animator
    Animator anim;
    GameObject player;

    int health = 100;

    // Vision variables
    // Range the agent can see
    public float visionRange;
    // Ray from the agent to the player
    RaycastHit hit;
    // Vision angle of the agent

    Vector3 playerLastPos;
    public float visionAngle;
    public float hearingDistance;

    // TODO Add rotationSpeed to this and pass it to BaseFSM
    // TODO Add Waypoint accuracy to this and pass it to BaseFSM

    // Method called from NPCBaseFSM
    public GameObject GetPlayer()
    {
        return player;
    }



    // Use this for initialization
    void Start()
    {
        // Get animator
        anim = GetComponent<Animator>();

        // Get player using their tag
        player = GameObject.FindGameObjectWithTag("Player");

        InvokeRepeating("UpdateHealth", 0.1f, 0.1f);
    }

    private void LateUpdate()
    {
         // Work out the vector from the bot to the player
        Vector3 distanceToPlayer = player.transform.position - this.transform.position;

        // Set the distanceToPlayer parameter
        anim.SetFloat("distanceToPlayer",
                      Vector3.Distance(transform.position,
                      player.transform.position));
        //Calculate Angle between direction vector above and the..
        //..forward facing vector of the NPC
        float angle = Vector3.Angle(distanceToPlayer, this.transform.forward);


        // Take health from the enemy. Used for testing
        if (Input.GetKeyDown("space"))
        {
            health -= 10;
        }
        anim.SetInteger("health", health);

        //Vision

        // Debug ray to see where the distance vector is being projected
        //Debug.DrawRay(this.transform.position, distanceToPlayer, Color.red);

        // Project Ray
        if (Physics.Raycast(this.transform.position, distanceToPlayer, out hit))
        {
            // If ray hits the player & is within vision range
            if (hit.collider.gameObject.tag == "Player" && distanceToPlayer.magnitude < visionRange && angle < visionAngle || distanceToPlayer.magnitude < hearingDistance)
            {
                anim.SetBool("detectPlayer", true);
            }
            else
            {
                anim.SetBool("detectPlayer", false);
            }
        }
    }

    // Method to update the health of the agent
    // Called when agent enters retreating or downed state
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

    public void HealRetreat()
    {
        health += 5;
    }

    public void HealDowned()
    {
        health += 5;
    }

}
