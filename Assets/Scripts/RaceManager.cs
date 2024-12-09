using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public GameObject[] racers;
    public Checkpoint[] checkpoints;
    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);

        // Assign checkpointIndex based on hierarchy order
        foreach (Checkpoint checkpoint in checkpoints)
        {
            checkpoint.checkpointIndex = checkpoint.transform.GetSiblingIndex();
            //print(checkpoint.name + " : " + checkpoint.checkpointIndex);
        }
        System.Array.Sort(checkpoints, (a, b) => a.checkpointIndex.CompareTo(b.checkpointIndex));
        
        for (int i = 0; i < racers.Length; i++)
        {
            racers[i].GetComponent<RacingData>().finalCheckpoint = checkpoints[^1].checkpointIndex;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < racers.Length; i++)
        {
            float distance = CalculateDistance(racers[i]);
            racers[i].GetComponent<RacingData>().UpdateRaceProgress(distance);
        }
        for (int i = 0; i < racers.Length; i++)
        {
            UpdatePlacement(racers[i]);
        }
    }

    public void UpdatePlacement(GameObject racer)
    {
        RacingData data = racer.GetComponent<RacingData>();
        int placement = 1;
        foreach (GameObject car in racers)
        {
            if (car != racer)
            {
                if (car.GetComponent<RacingData>().raceProgress > data.raceProgress)
                {
                    placement++;
                }
            }
        }
        data.placement = placement;
    }

    public float CalculateDistance(GameObject racer)
    {
        RacingData data = racer.GetComponent<RacingData>();
        int nextCheckpointIndex = data.currentCheckpoint + 1;

        // Get the position of the next checkpoint
        Transform nextCheckpoint = checkpoints[nextCheckpointIndex].GameObject().transform;

        // Calculate distance to the next checkpoint
        float distance = Vector3.Distance(racer.transform.position, nextCheckpoint.position);
        return distance;
    }
}
