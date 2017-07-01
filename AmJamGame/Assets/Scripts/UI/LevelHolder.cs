using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenuAttribute(fileName = "LevelHolder")]
public class LevelHolder : ScriptableObject
{
    
    public string menuScene;
   
    public List<string> listOfScenes;


    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(listOfScenes[index]);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

}

