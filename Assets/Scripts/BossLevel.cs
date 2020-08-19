using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevel : MonoBehaviour
{
    public float nextLevelTime = 2;
    public string nextLevelName;
    public GameObject endOfLevelText;

    void Start()
    {
        this.endOfLevelText.SetActive(false);
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            this.OnEnemiesKilled();
        }
    }

    void OnEnemiesKilled()
    {
        this.endOfLevelText.SetActive(true);
        Invoke("LoadNextScene", this.nextLevelTime);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(this.nextLevelName);
    }
}
