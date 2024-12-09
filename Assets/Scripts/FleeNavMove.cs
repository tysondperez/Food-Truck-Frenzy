using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeNavMove : MonoBehaviour
{
    [SerializeField] Transform player;
    NavMeshAgent agent;
    Animator animator;

    [SerializeField] Transform[] navPoints;
    float fleeRadius = 200f;
    float maxDistance;
    Transform bestPoint;
    float playerDistance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GetPoint();
    }

    void Update()
    {
        animator.SetFloat("Speed", agent.speed);
        //agent.SetDestination(transform.position - (player.position - transform.position));

        if (bestPoint != null)
        {
            if (Vector3.Distance(transform.position, bestPoint.position) < 2f)
            {
                GetPoint();
            }
            agent.SetDestination(bestPoint.position);
        }
    }

    void GetPoint()
    {
        foreach (Transform t in navPoints)
        {
            playerDistance = Vector3.Distance(t.position, player.position);

            if (playerDistance > fleeRadius)
            {
                if(playerDistance > maxDistance)
                {
                    maxDistance = playerDistance;
                    bestPoint = t;
                }
            }
        }
    }
}
