using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor; // Only included in the editor
#endif

public class BootstraperProxy : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        print("triggered");
        print("Active scene in proxy: " + SceneManager.GetActiveScene().name);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SwitchScene(SceneManager.GetActiveScene().name, sceneName);
        }
        else
        {
            Debug.LogError("GameManager is not initialized!");
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        // Exit play mode in the editor
        EditorApplication.isPlaying = false;
#else
        // Quit the application in a build
        Application.Quit();
#endif
        Debug.Log("Application has quit.");
    }
}
