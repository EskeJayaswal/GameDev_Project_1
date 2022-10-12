using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;

    public GameObject menu;
    public GameObject HUDCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
            menu.SetActive(true);
            HUDCanvas.SetActive(false);

        }
        else 
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            HUDCanvas.SetActive(true);
        }
    }
        public void PrintButton() 
    {
        Debug.Log("Hello");
    
    }
}


