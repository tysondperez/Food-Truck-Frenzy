using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingNavMove : MonoBehaviour
{
    public Transform[] points;
    public Transform[] outerPoints;
    private int destPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        //agent.autoBraking = false;

        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    void GotoNextPoint() {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = (Random.Range(1,2) == 1 ? points[destPoint].position : outerPoints[destPoint].position);
        //agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
}
