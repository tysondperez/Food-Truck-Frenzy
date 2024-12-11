using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlacementTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI placeText; // Drag your TextMeshPro object here
    public TextMeshProUGUI lapText; // Drag your TextMeshPro object here
    public TextMeshProUGUI checkText; // Drag your TextMeshPro object here
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RacingData data = player.GetComponent<RacingData>();
        int place = data.placement;
        switch (place)
        {
            case 1: placeText.text = "1st"; break;
            case 2: placeText.text = "2nd"; break;
            case 3: placeText.text = "3rd"; break;
            case 4: placeText.text = "4th"; break;
        }
        lapText.text = "Lap " + (data.currentLap + 1) + "/" + data.numLaps;
        checkText.text = "Checkpoint " + (data.currentCheckpoint + 1) + "/" + data.finalCheckpoint;
    }
}
