using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstSceneName;

    public void OnPlay()
    {
        SceneManager.LoadScene(this.firstSceneName);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
