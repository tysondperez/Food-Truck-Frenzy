using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    //player data goes here?
    public bool playerHasBoost;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Directly start loading the first scene
        StartCoroutine(LoadSceneAsync("MainMenu"));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Set it as the active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        print("Active scene in Async: " + SceneManager.GetActiveScene().name);
    }

    public void SwitchScene(string currentScene, string newScene)
    {
        StartCoroutine(SwitchSceneAsync(currentScene, newScene));
    }

    private IEnumerator SwitchSceneAsync(string currentScene, string newScene)
    {
        // Load the new scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        // Wait until the new scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Once the new scene is loaded, set it as the active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));

        // Unload the old scene
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentScene);

        // Wait until the old scene is fully unloaded
        while (!asyncUnload.isDone)
        {
            yield return null;
        }

        print("Switched to new scene: " + newScene);
    }
}
