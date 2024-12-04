using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
