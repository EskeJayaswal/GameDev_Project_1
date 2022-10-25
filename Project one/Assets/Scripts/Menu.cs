using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnNewGameButton()
    {
        SceneManager.LoadScene("Game Night");
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
