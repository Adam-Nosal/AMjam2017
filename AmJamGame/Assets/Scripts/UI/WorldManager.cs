using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 

public class WorldManager : Singleton<WorldManager>
{
    [SerializeField]
    private LevelHolder levelHolder;
    private string levelHolderPath = "mainLevelHolder";
    private int currentLevel = 0;                                  //Current level number
    
    void Awake()
    {
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);

        InitLevelHolder();
    }

    void InitLevelHolder()
    {
        if (levelHolder == null)
        {
            levelHolder = Resources.Load(levelHolderPath) as LevelHolder;
        }
    }

    //Initializes the game for each level.
    public void InitGame()
    {
        InitLevelHolder();
        levelHolder.LoadLevel(currentLevel);

    }

    public void LoadNextLevel()
    {
        InitLevelHolder();
        currentLevel++;
        levelHolder.LoadLevel(currentLevel);

    }
}


    