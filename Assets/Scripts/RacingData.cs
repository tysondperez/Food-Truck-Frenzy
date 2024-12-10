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
                if (CompareTag("Player"))
                {
                   other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                   RaceManager.checkpoints[0].gameObject.GetComponent<MeshRenderer>().enabled = true; 
                }
            }
            else if (currentCheckpoint == newInd - 1)
            {
                currentCheckpoint = newInd;
                if (CompareTag("Player"))
                {
                    other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    RaceManager.checkpoints[newInd + 1].gameObject.GetComponent<MeshRenderer>().enabled = true; 
                }
            } 
            else if (currentCheckpoint == 0 && newInd == 0)
            {
                if (CompareTag("Player"))
                {
                    other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    RaceManager.checkpoints[newInd + 1].gameObject.GetComponent<MeshRenderer>().enabled = true; 
                }
            }
            //print(currentCheckpoint);
        }
    }
}
