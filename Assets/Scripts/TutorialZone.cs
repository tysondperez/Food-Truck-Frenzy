using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZone : MonoBehaviour
{
    public int tutorialIndex;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("hi");
            TutorialManager tutorialManager = FindObjectOfType<TutorialManager>();
            if (tutorialManager != null )
            {
                tutorialManager.OnPlayerEnter(tutorialIndex);
            }

            Destroy(gameObject);
        }
    }
}
