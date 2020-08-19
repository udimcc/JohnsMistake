using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    public float nextLevelTime = 2;
    public string nextLevelName;
    public int currentLevelIndex;
    public GameObject endOfLevelText;

    void Start()
    {
        this.endOfLevelText.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            return;
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            return;
        }

        this.endOfLevelText.SetActive(true);
        Invoke("LoadNextScene", this.nextLevelTime);
    }

    void LoadNextScene()
    {
        if (GameState.Instance.GetNextLevelIndex() == this.currentLevelIndex)
        {
            GameState.Instance.IncreaseNextLevelIndex();
        }

        SceneManager.LoadScene(this.nextLevelName);
    }
}
