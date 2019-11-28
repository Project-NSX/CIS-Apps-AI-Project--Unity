using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player's movement speed
    float moveSpeed;
    // player's rotation speed
    public float rotationSpeed = 120.0F;

    // player's movement speed while in mud
    public float mudSpeed = 2;
    // player's normal movement speed
    public float normalSpeed = 7;
    // Bool for if the player is in the mud or not
    bool inMud;

    // Trigger from collider - triggered when the player enters mud collider
    public void OnTriggerEnter(Collider other)
    {
        // If the tag of the collider is mud
        if (other.tag == "Mud")
        {
            //Debug.Log("Player in mud!");
            // Set player's in mud bool to true
            inMud = true;
        }
    }

    // Trigger from collider - triggered when the player exits mud collider
    public void OnTriggerExit(Collider other)
    {
        // if collider tag is mud
        if (other.tag == "Mud")
        {
            // player is no longer in mud
            inMud = false;
        }
    }

    void Update()
    {
        // If player is in mud
        if (inMud == true)
        {
            // Movement speed = the set speed for being in mud
            moveSpeed = mudSpeed;
        }
        else
        {
            // player is not in mud - set speed to normal
            moveSpeed = normalSpeed;
        }

        // Player's controller settings - Allows the player to move and rotate
        float translation = Input.GetAxis("Vertical") * moveSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

    }
}
