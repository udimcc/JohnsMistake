using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    static GameState instance = null;
    int nextLevelIndex = 0;


    private GameState() { }

    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameState();
            }
            return instance;
        }
    }

    public int GetNextLevelIndex()
    {
        return this.nextLevelIndex;
    }

    public void IncreaseNextLevelIndex()
    {
        this.nextLevelIndex++;
    }
}
