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
    float minimumAngle = 90f;

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
            if (Vector3.Distance(transform.position, bestPoint.position) < 30f)
            {
                GetPoint();
            }
            agent.SetDestination(bestPoint.position);
        }
    }

    void GetPoint()
    {
        System.Collections.Generic.List<Transform> validPoints = new System.Collections.Generic.List<Transform>();

        foreach (Transform t in navPoints)
        {
            /* playerDistance = Vector3.Distance(t.position, player.position);

             if (playerDistance > fleeRadius)
             {
                 if(playerDistance > maxDistance)
                 {
                     maxDistance = playerDistance;
                     bestPoint = t;
                 }
             }*/

            Vector3 directionToSafePoint = (t.position - transform.position).normalized;

            // Calculate the direction from the NPC to the player.
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Calculate the angle between the direction to the safe point and the direction to the player.
            float angle = Vector3.Angle(directionToSafePoint, directionToPlayer);

            // Only add the safe point to the list if it's not in the general direction of the player
            if (angle > minimumAngle)
            {
                validPoints.Add(t);
            }
        }

        // If there are any valid points, choose a random one.
        if (validPoints.Count > 0)
        {
            bestPoint = validPoints[Random.Range(0, validPoints.Count)];
        }
        else
        {
            // If no valid points, just choose a random one from the list regardless of the angle.
            bestPoint = navPoints[Random.Range(0, navPoints.Length)];
        }
    }
}
