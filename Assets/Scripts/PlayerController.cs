using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed;
    public float rotationSpeed = 120.0F;

    public float mudSpeed = 2;
    public float normalSpeed = 7;
    bool inMud;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mud")
        {
            Debug.Log("MUD");
            inMud = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mud")
        {
            inMud = false;
        }

    }
    void Update()
    {
        if (inMud == true)
        {
            moveSpeed = mudSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }

        float translation = Input.GetAxis("Vertical") * moveSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

    }
}
