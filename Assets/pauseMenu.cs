using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI; 

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
        {
            GameIsPaused = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Registered");
            if (GameIsPaused == true)
            {
                Resume();
                Debug.Log("Game is playing");
            }
            else if (GameIsPaused == false)
            {
                Pause();
                Debug.Log("Game is paused");
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
