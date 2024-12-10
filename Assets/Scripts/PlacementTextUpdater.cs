using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlacementTextUpdater : MonoBehaviour
{
    public TextMeshProUGUI text; // Drag your TextMeshPro object here
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int place = player.GetComponent<RacingData>().placement;
        switch (place)
        {
            case 1: text.text = "1st"; break;
            case 2: text.text = "2nd"; break;
            case 3: text.text = "3rd"; break;
            case 4: text.text = "4th"; break;
        }
    }
}
