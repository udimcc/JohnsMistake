using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChooser : MonoBehaviour
{
    public string firstSceneName;
    public string secondSceneName;
    public string thirdSceneName;
    public string arcadeSceneName;

    public Button secondLevel;
    public Button thirdLevel;


    private void Start()
    {
        int nextLevelIndex = GameState.Instance.GetNextLevelIndex();

        if (nextLevelIndex < 2)
        {
            this.thirdLevel.interactable = false;
        }

        if (nextLevelIndex < 1)
        {
            this.secondLevel.interactable = false;
        }
    }

    public void OnFirstLevelClick()
    {
        SceneManager.LoadScene(this.firstSceneName);
    }

    public void OnSecondLevelClick()
    {
        SceneManager.LoadScene(this.secondSceneName);
    }

    public void OnThirdLevelClick()
    {
        SceneManager.LoadScene(this.thirdSceneName);
    }

    public void OnArcadeClick()
    {
        SceneManager.LoadScene(this.arcadeSceneName);
    }
}
