using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingNavMove : MonoBehaviour
{
    public Transform[]  innerPoints;
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
        if (!agent.pathPending && agent.remainingDistance < 20f)
        {
            GotoNextPoint();
        }
        anim.SetFloat("Speed", agent.velocity.magnitude);
        
        if (agent.velocity.magnitude > 0.1f) // Avoid unnecessary rotation when nearly stationary
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Adjust the 5f for faster/slower rotation
        }
        print(agent.velocity);
    }

    void GotoNextPoint() {
        // Returns if no  innerPoints have been set up
        if ( innerPoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = (Random.Range(1, 3) == 1 ?  innerPoints[destPoint].position : outerPoints[destPoint].position);
        //agent.destination =  innerPoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) %  innerPoints.Length;
    }
}
