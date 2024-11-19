using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private float pausedTimeScale;
    public bool isPaused;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            //print("Triggered"); 
            if (isPaused){
                Unpause();
            } else {
                Pause();
            }
        }
    }

    public void Pause(){
        panel.SetActive(true);
        isPaused = true;
        pausedTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public void Unpause(){
        panel.SetActive(false);
        isPaused = false;
        Time.timeScale = pausedTimeScale;
    }
}