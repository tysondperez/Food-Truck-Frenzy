using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingNavMove : MonoBehaviour
{
    public Transform[]  innerPoints;
    public Transform[] outerPoints;
    private int destPoint = 0;
    public UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;
    
    public float catchupBoost = 0f;
    public float megaBoost = 0f;
    
    public float baseSpeed = 40f;

    public bool boostCapable;
    public float tacticalBoost = 0f;
    public bool tacBoosting = false;
    public float maxBoostChance = 0.8f;
    public float boostCooldown = 15f;
    public float boostDuration = 5f;
    public float decisionDelay = 2f;
    private bool canCheckBoost = true;
    
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
        
        Vector3 agentVelocity = agent.velocity.normalized;
        Vector3 forward = transform.forward;
        float turnDirection = Vector3.Cross(forward, agentVelocity).y * 80f;
        // Use turnDirection to update the animation parameter
        anim.SetFloat("Direction", turnDirection);
        
        if (agent.velocity.magnitude > 0.1f) // Avoid unnecessary rotation when nearly stationary
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Adjust the 5f for faster/slower rotation
        }

        if (!tacBoosting && boostCapable && canCheckBoost)
        {
            StartCoroutine(DecideTacBoost());
            canCheckBoost = false;
        }
        
        if (boostCapable)
        {
            agent.speed = baseSpeed + catchupBoost + megaBoost + tacticalBoost;
        }
        else
        {
            agent.speed = baseSpeed + catchupBoost + megaBoost;
        }
        
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

    public IEnumerator DecideTacBoost()
    {
        RacingData data = GetComponent<RacingData>();
        float percentDone = (float) data.currentCheckpoint / data.finalCheckpoint;
        float boostChance = Mathf.Lerp(0.01f, maxBoostChance, percentDone);
        float rand = Random.value;
        if (rand < boostChance)
        {
            StartBoost();
        }
        
        yield return new WaitForSeconds(decisionDelay);
        canCheckBoost = true;
    }

    public void StartBoost()
    {
        tacBoosting = true;
        tacticalBoost = 30f;
        if (transform.Find("Particles") != null)
        {
            transform.Find("Particles").gameObject.SetActive(true);
        }
        StartCoroutine(TurnBoostOff());
    }

    private IEnumerator TurnBoostOff()
    {
        yield return new WaitForSeconds(boostDuration);
        tacticalBoost = 0f;
        if (transform.Find("Particles") != null)
        {
            transform.Find("Particles").gameObject.SetActive(false);
        }
        StartCoroutine(BoostCooldown());
    }

    private IEnumerator BoostCooldown()
    {
        yield return new WaitForSeconds(boostCooldown);
        tacBoosting = false;
    }
}
