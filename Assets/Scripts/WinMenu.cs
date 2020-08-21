using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public string mainMenuSceneName;


    public void OnMainMenuClick()
    {
        SceneManager.LoadScene(this.mainMenuSceneName);
    }
}
