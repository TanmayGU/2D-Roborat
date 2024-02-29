using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject pauseUI;
    //public GameObject pbutton;
    //public GameObject resume;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }



    //public void pause()
    //{
    //    pauseUI.SetActive(true);
    //    Time.timeScale = 0;
    //}

    public void resume()
    {
        Debug.Log("Resume");
        //pbutton.resume();
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void mainmenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void quit()
    {
        Application.Quit();
    }
}