using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]


// This script was made from: https://www.youtube.com/watch?v=nrRfqS6u_zg
public class PathDebug : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agentToDebug;

    private LineRenderer linerenderer;
    // Start is called before the first frame update
    void Start()
    {
        linerenderer = GetComponent<LineRenderer>();
        agentToDebug = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agentToDebug.hasPath)
        {
            linerenderer.positionCount = agentToDebug.path.corners.Length;
            linerenderer.SetPositions(agentToDebug.path.corners);
            linerenderer.enabled = true;
        }
        else
        {
            linerenderer.enabled = false;
        }


    }
}
