using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuCommandInterpreter : Singleton<MenuCommandInterpreter>
{
    Actor actor;
    List<string> commandsList = new List<string>()
        {
            "start",
            "exit",
            "credits",
            "shake"
        };

    WorldManager worldManager;
    MenuManager menuManager;


    // Use this for initialization
    void Awake()
    {
        //commandsList = new List<string>()
        //{
        //    "move",
        //    "interact",
        //    "possess"
        //};

        worldManager = WorldManager.Instance;
        menuManager = MenuManager.Instance;
    }

    public List<string> usedCommandsList = new List<string>();

    // Update is called once per frame
    void Update()
    {
       
    }

    public void InterpretCommands()
    {
        List<ActorCommand> commands = new List<ActorCommand>();        

        //if (usedCommandsList.Count == 0)
        //{
        //    ConsoleMenu.Instance.AddFeedback(0,  "Type sth you idiot!");
        //    return;
        //}        

        for (int i = 0; i < usedCommandsList.Count; i++)
        {
          
            if (usedCommandsList[i].IndexOf('(') < 0)
            {
                ConsoleMenu.Instance.AddFeedback(i,  "you forgot about brackets again...");
                return;
            }

            string commandName = usedCommandsList[i].Substring(0, usedCommandsList[i].IndexOf('('));

            if (commandsList.Find(x => x == commandName) == null) // command not found
            {
                ConsoleMenu.Instance.AddFeedback(i, "Command not found!");
                return;
            }

            switch (commandName)
            {
                case "start":
                    {
                        worldManager.InitGame();
                        break;
                    }
                case "exit":
                    {
                        Application.Quit();
                        break;
                    }
                case "credits":
                    {
                        menuManager.PrintCredits();
                        break;
                    }
                case "shake":
                    {
                        Camera.main.GetComponent<CameraControl>().Shake(5.0f, 10, 10);
                        break;
                    }
            }

         
        }


    }
    
}
