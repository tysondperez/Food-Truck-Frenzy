using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStarter : MonoBehaviour
{
    [SerializeField] MonoBehaviour aiMovement;
    [SerializeField] MonoBehaviour playerMovement;

    [SerializeField] Light red;
    [SerializeField] Light yellow;
    [SerializeField] Light green;

    float countdown = 3f;
    bool countActive = false;

    void Awake()
    {
        DisableCharacters();
        countActive = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(countdown);
        if (countActive)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
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
        aiMovement.enabled = false;
        playerMovement.enabled = false;
    }

    void EnableCharacter()
    {
        aiMovement.enabled = true;
        playerMovement.enabled = true;
    }
}
