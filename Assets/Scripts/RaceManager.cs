using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public GameObject[] racers;
    public static Checkpoint[] checkpoints;
    public bool switching = false;
    private readonly float[] boosts = {
        0f, 5f, 10f, 15f
    };

    private readonly float megaBoost = 15f;

    public Canvas winCanvas;
    public TextMeshProUGUI winText;
    
    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);

        // Assign checkpointIndex based on hierarchy order
        foreach (Checkpoint checkpoint in checkpoints)
        {
            checkpoint.checkpointIndex = checkpoint.transform.GetSiblingIndex();
        }
        System.Array.Sort(checkpoints, (a, b) => a.checkpointIndex.CompareTo(b.checkpointIndex));
        
        for (int i = 0; i < racers.Length; i++)
        {
            RacingData data = racers[i].GetComponent<RacingData>();
            data.finalCheckpoint = checkpoints[^1].checkpointIndex;
        }
        print("started");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < racers.Length; i++)
        {
            float distance = CalculateDistance(racers[i]);
            racers[i].GetComponent<RacingData>().UpdateRaceProgress(distance);
        }
        
        GameObject firstPlaceRacer = GetFirstPlaceRacer();
        
        for (int i = 0; i < racers.Length; i++)
        {
            int place = UpdatePlacement(racers[i]);
            RacingData racerData = racers[i].GetComponent<RacingData>();

            // Calculate distance to the first-place racer
            float distanceToFirst = Vector3.Distance(racers[i].transform.position, firstPlaceRacer.transform.position);

            // Apply catch-up boost only if the racer is 20 or more units away from 1st place
            if (distanceToFirst >= 20f)
            {
                if (racers[i].CompareTag("Player"))
                {
                    racers[i].GetComponent<TruckMovement>().catchupBoost = boosts[place - 1];
                }
                else
                {
                    racers[i].GetComponent<RacingNavMove>().catchupBoost = boosts[place - 1];
                }

                if (distanceToFirst >= 300f && !racerData.megaBoosting)
                {
                    racerData.megaBoosting = true;
                    if (racers[i].CompareTag("Player"))
                    {
                        racers[i].GetComponent<TruckMovement>().megaBoost = megaBoost;
                    }
                    else
                    {
                        racers[i].GetComponent<RacingNavMove>().megaBoost = megaBoost;
                    }
                    print("racer "+i+" is outside of 300m, enabling mega boost");
                }

                if (distanceToFirst <= 50f && racerData.megaBoosting)
                {
                    racerData.megaBoosting = false;
                    if (racers[i].CompareTag("Player"))
                    {
                        racers[i].GetComponent<TruckMovement>().megaBoost = 0f;
                    }
                    else
                    {
                        racers[i].GetComponent<RacingNavMove>().megaBoost = 0f;
                    }
                    print("racer "+i+" is inside of 50m, disabling mega boost");
                }
            }
            else
            {
                // Reset boost if the condition is not met
                if (racers[i].CompareTag("Player"))
                {
                    racers[i].GetComponent<TruckMovement>().catchupBoost = 0f;
                }
                else
                {
                    racers[i].GetComponent<RacingNavMove>().catchupBoost = 0f;
                }
            }
        }
        
        RacingData playerData = racers[0].GetComponent<RacingData>();
        if (playerData.lapsCompleted == (playerData.totalLaps) && !switching)
        {
            racers[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            racers[0].GetComponent<TruckMovement>().enabled = false;
            for (int i = 0; i < racers.Length; i++)
            {
                racers[i].GetComponent<RacingNavMove>().enabled = false;
            }

            ShowRaceEnd(playerData.placement);
            StartCoroutine(Switch());
            switching = true;
        }
        
    }

    public void ShowRaceEnd(int placement)
    {
        switch (placement)
        {
            case 1: winText.text = "You finished the race! You placed 1st!";
                break;
            case 2: winText.text = "You finished the race! You placed 2nd!";
                break;
            case 3: winText.text = "You finished the race! You placed 3rd.";
                break;
            case 4: winText.text = "You finished the race! You placed 4th.";
                break;
        }
        winCanvas.gameObject.SetActive(true);
    }
    
    private IEnumerator Switch()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.SwitchScene(SceneManager.GetActiveScene().name, "LevelSelect");
    }

    public GameObject GetFirstPlaceRacer()
    {
        GameObject firstPlaceRacer = null;
        float highestProgress = float.MinValue;

        foreach (GameObject racer in racers)
        {
            float progress = racer.GetComponent<RacingData>().raceProgress;
            if (progress > highestProgress)
            {
                highestProgress = progress;
                firstPlaceRacer = racer;
            }
        }

        return firstPlaceRacer;
    }

    public int UpdatePlacement(GameObject racer)
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
        return placement;
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
