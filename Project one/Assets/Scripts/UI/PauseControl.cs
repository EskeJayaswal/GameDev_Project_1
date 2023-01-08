using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;

    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject HUDCanvas;

    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject optionsScreen;

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
        pauseScreen.SetActive(false);
        optionsScreen.SetActive(true);
    
    }

    public void OnOptionsBackButton() 
    {
        pauseScreen.SetActive(true);
        optionsScreen.SetActive(false);
    
    }

    public void OnQuitButton() 
    {

        PauseGame();
        SceneManager.LoadScene("Menu");
    
    }


}


