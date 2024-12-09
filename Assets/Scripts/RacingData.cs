using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingData : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentCheckpoint;
    public int finalCheckpoint;
    public int currentLap;

    public float raceProgress;
    public int placement = 1;

    // Update is called once per frame
    public void UpdateRaceProgress(float distance)
    {
        raceProgress = currentLap * 50000f; // Give laps high weight
        raceProgress += currentCheckpoint * 1000f; // Medium weight for checkpoints
        raceProgress -= distance; // Lower distance is better
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            int newInd = other.gameObject.GetComponent<Checkpoint>().checkpointIndex;
            if (newInd == finalCheckpoint && currentCheckpoint == finalCheckpoint - 1)
            {
                currentLap++;
                currentCheckpoint = 0;
            }
            else if (currentCheckpoint == newInd - 1)
            {
                currentCheckpoint = newInd;
            }
            print(currentCheckpoint);
        }
    }
}
