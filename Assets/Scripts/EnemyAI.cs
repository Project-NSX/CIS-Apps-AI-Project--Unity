using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Initialise animator
    Animator anim;
    GameObject player;

    int health = 100;

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
        // Set the distanceToPlayer parameter
        anim.SetFloat("distanceToPlayer",
                      Vector3.Distance(transform.position,
                      player.transform.position));

        // Take health from the enemy. Used for testing
        if (Input.GetKeyDown("space"))
        {
            health -= 10;       
        }
        anim.SetInteger("health", health);
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
