using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Text tutorialText;  // Reference to the UI Text element
    public string[] tutorialSteps;  // Array of tutorial instructions
    private int currentStep = 0;  // Keeps track of the current step in the tutorial

    public MonoBehaviour tutorialAI;

    public bool isPaused = false;
    private float pausedTimeScale;
    public GameObject panel;

    void Awake()
    {
        tutorialAI.enabled = false;
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
            tutorialAI.enabled = true;
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
            else
            {
                EndTutorial();
            }
        }
    }

    void EndTutorial()
    {
        tutorialText.text = "That's the tutorial! Now you've learned everything you need to finish this race out! Go get 'em!";

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
