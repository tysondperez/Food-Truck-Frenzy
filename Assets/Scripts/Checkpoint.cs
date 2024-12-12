using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public int checkpointIndex = -1;
    public bool diverges = false;
    public AltCheckpoint altCheckpoint;
}
