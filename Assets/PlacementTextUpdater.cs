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
        text.text = "" + player.GetComponent<RacingData>().placement;
    }
}
