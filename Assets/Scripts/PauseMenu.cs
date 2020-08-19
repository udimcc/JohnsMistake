using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenu;
    public string mainMenuSceneName;

    CursorLockMode lastCursorMode;


    private void Start()
    {
        this.lastCursorMode = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            this.UpdatePause();
        }
    }

    public void OnResumeButton()
    {
        isGamePaused = false;
        this.UpdatePause();
    }

    public void OnQuitToMenu()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(this.mainMenuSceneName);
    }

    private void UpdatePause()
    {
        if (isGamePaused)
        {
            this.pauseMenu.SetActive(true);
            Time.timeScale = 0;
            this.lastCursorMode = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            this.pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = this.lastCursorMode;
        }
    }
}
