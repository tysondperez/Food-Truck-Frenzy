using System.Collections;
using System.Collections.Generic;
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
            print(checkpoint.name + " : " + checkpoint.checkpointIndex);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
