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
    Transform bestPoint;
    float minimumAngle = 90f;
    
    private float pausedTimeScale;

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
            Vector3 directionToSafePoint = (t.position - transform.position).normalized;

            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            float angle = Vector3.Angle(directionToSafePoint, directionToPlayer);

            if (angle > minimumAngle)
            {
                validPoints.Add(t);
            }
        }

        if (validPoints.Count > 0)
        {
            bestPoint = validPoints[Random.Range(0, validPoints.Count)];
        }
        else
        {
            bestPoint = navPoints[Random.Range(0, navPoints.Length)];
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.playerHasBoost = true;

            this.enabled = false;
            player.GetComponent<TruckMovement>().enabled = false;
            StartCoroutine(Switch());
        }
    }

    private IEnumerator Switch()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.SwitchScene("ChaseLevel", "LevelSelect");
    }
}
