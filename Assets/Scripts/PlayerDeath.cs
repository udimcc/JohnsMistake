using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour, Killable
{
    public Text gameOverText;
    public string menuSceneName;
    public float menuSceneTime = 2f;

    public void OnDeath()
    {
        this.gameObject.SetActive(false);
        
        this.gameOverText.gameObject.SetActive(true);
        Invoke("LoadMenuScene", this.menuSceneTime);
    }

    void LoadMenuScene()
    {
        SceneManager.LoadScene(this.menuSceneName);
    }
}
