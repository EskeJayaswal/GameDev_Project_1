using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;

    public GameObject menu;
    public GameObject HUDCanvas;

    public static PauseControl instance;

    void Start()
    {
        instance = this;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame ()
    {
        gameIsPaused = !gameIsPaused;
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
            menu.SetActive(true);
            HUDCanvas.SetActive(false);
            // Unlock the cursor, so we can click on button etc.
            Cursor.lockState = CursorLockMode.None;

        }
        else 
        {
            Time.timeScale = 1;
            menu.SetActive(false);
            HUDCanvas.SetActive(true);

            // Put the cursor lock back on
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnContinueButton() 
    {

        PauseGame();
    
    }

    public void OnOptionsButton() 
    {
        Debug.Log("No Options yet..");
    
    }

    public void OnQuitButton() 
    {

        PauseGame();
        SceneManager.LoadScene("Menu");
    
    }


}


