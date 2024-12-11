using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStarter : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] MovementScripts;

    [SerializeField] Light red;
    [SerializeField] Light yellow;
    [SerializeField] Light green;

    public AudioSource audioSource;
    public AudioClip countdownSound;

    float countdown = 3f;
    bool countActive = false;

    void Awake()
    {
        DisableCharacters();
        countActive = true;
        if (audioSource != null && countdownSound != null)
        {
            audioSource.PlayOneShot(countdownSound);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(countdown);
        if (countActive)
        {
            countdown -= Time.deltaTime;
            if (countdown <= .5)
            {
                EnableCharacter();
                green.enabled = true;
                yellow.enabled = false;
                countActive = false;
            } else if (countdown <= 1.5)
            {
                red.enabled = false;
                yellow.enabled = true;
            }
        }
    }

    void DisableCharacters()
    {
        foreach (MonoBehaviour script in MovementScripts)
        {
            script.enabled = false;
        }
    }

    void EnableCharacter()
    {
        foreach (MonoBehaviour script in MovementScripts)
        {
            script.enabled = true;
        }
    }
}
