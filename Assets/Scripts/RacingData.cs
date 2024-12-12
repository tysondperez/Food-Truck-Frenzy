using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RacingData : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentCheckpoint;
    public int finalCheckpoint;
    [FormerlySerializedAs("currentLap")] public int lapsCompleted = 1;
    public readonly int totalLaps = 3;

    public float raceProgress;
    public int placement = 1;

    public bool megaBoosting;
    
    public AudioSource audioSource;
    public AudioClip checkpointSound;

    public RaceManager manager;

    private void Start()
    {
        manager = FindObjectOfType<RaceManager>();
    }

    // Update is called once per frame
    public void UpdateRaceProgress(float distance)
    {
        raceProgress = lapsCompleted * 50000f; // Give laps high weight
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
                lapsCompleted++;
                currentCheckpoint = 0;
                if (CompareTag("Player"))
                {
                   other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                   manager.checkpoints[0].gameObject.GetComponent<MeshRenderer>().enabled = true;
                   if (audioSource != null && checkpointSound != null)
                   {
                       audioSource.PlayOneShot(checkpointSound);
                   }
                }
            }
            else if (currentCheckpoint == newInd - 1)
            {
                currentCheckpoint = newInd;
                if (CompareTag("Player"))
                {
                    other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    manager.checkpoints[newInd + 1].gameObject.GetComponent<MeshRenderer>().enabled = true;
                    if (other.GetComponent<Checkpoint>().altCheckpoint != null)
                    {
                        other.GetComponent<Checkpoint>().altCheckpoint.GetComponent<MeshRenderer>().enabled = false;
                        if (manager.checkpoints[newInd + 1].gameObject.GetComponent<Checkpoint>().altCheckpoint != null)
                        {
                            manager.checkpoints[newInd + 1].gameObject.GetComponent<Checkpoint>().altCheckpoint.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                    if (audioSource != null && checkpointSound != null)
                    {
                        audioSource.PlayOneShot(checkpointSound);
                    }
                }
            } 
            else if (currentCheckpoint == 0 && newInd == 0)
            {
                if (CompareTag("Player"))
                {
                    other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    manager.checkpoints[newInd + 1].gameObject.GetComponent<MeshRenderer>().enabled = true; 
                    if (audioSource != null && checkpointSound != null)
                    {
                        audioSource.PlayOneShot(checkpointSound);
                    }
                }
            }
            //print(currentCheckpoint);
        } 
        else if (other.CompareTag("AltCheckpoint"))
        {
            int newInd = other.gameObject.GetComponent<AltCheckpoint>().checkpointIndex;
            if (currentCheckpoint == newInd - 1)
            {
                currentCheckpoint = newInd;
                if (CompareTag("Player"))
                {
                    other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    
                    manager.checkpoints[newInd + 1].gameObject.GetComponent<MeshRenderer>().enabled = true;
                    
                    manager.checkpoints[newInd].gameObject.GetComponent<MeshRenderer>().enabled = false;
                    
                    if (manager.checkpoints[newInd + 1].gameObject.GetComponent<Checkpoint>().altCheckpoint != null)
                    {
                        manager.checkpoints[newInd + 1].gameObject.GetComponent<Checkpoint>().altCheckpoint.GetComponent<MeshRenderer>().enabled = true;
                    }
                    if (audioSource != null && checkpointSound != null)
                    {
                        audioSource.PlayOneShot(checkpointSound);
                    }
                }
            } 
        }
    }
}
