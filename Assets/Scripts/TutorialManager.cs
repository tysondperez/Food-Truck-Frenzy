using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text tutorialText;  // Reference to the UI Text element
    public string[] tutorialSteps;  // Array of tutorial instructions
    public int currentStep = 0;  // Keeps track of the current step in the tutorial

    [SerializeField] GameObject[] tutorialAI;

    public bool isPaused = false;
    private float pausedTimeScale;
    public GameObject panel;

    void Awake()
    {
        
    }
    void Start()
    {
        // Start with the first tutorial step
        DisplayTutorialStep();
    }

    void Update()
    {
        // Listen for player input to move to the next tutorial step (e.g., pressing a key)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPaused)
            {
                Unpause();
            }
        }
    }

    void DisplayTutorialStep()
    {
        // Display the current tutorial step
        if (currentStep < tutorialSteps.Length)
        {
            tutorialText.text = tutorialSteps[currentStep];
        }
        else
        {
            tutorialText.text = "";  // Clear the tutorial text when finished
        }

        if (currentStep == 4)
        {
            foreach (GameObject truck in tutorialAI)
            {
                if (truck != null)
                {
                    truck.SetActive(true);
                }
            }
        }

        if (currentStep == 5)
        {
            tutorialAI[0].GetComponent<RacingNavMove>().boostCapable = true;
        }
    }

    void NextTutorialStep()
    {
        // Increment to the next step
        currentStep++;

        // Display the next tutorial step
        DisplayTutorialStep();
    }

    public void OnPlayerEnter(int tutorialStep)
    {
        if (tutorialStep == currentStep)
        {
            currentStep++;
            Pause();
            if (currentStep < tutorialSteps.Length)
            {
                DisplayTutorialStep();
            }
        }
        
        
    }

    public void Pause()
    {
        panel.SetActive(true);
        isPaused = true;
        pausedTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        panel.SetActive(false);
        isPaused = false;
        Time.timeScale = pausedTimeScale;
    }
}
