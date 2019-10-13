using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Initialise animator
    Animator anim;
    
    // Use this for initialization
    void Start()
    {
        // Get animator
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

    }
}
