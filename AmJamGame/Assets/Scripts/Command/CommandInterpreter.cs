using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum directionType { up, down, left, right }

public class CommandInterpreter : Singleton<CommandInterpreter>
{

    List<string> commandsList;
    // Use this for initialization
    void Start()
    {
        commandsList = new List<string>()
        {
            "move",
            "interact",
            "possess"
        };
    }

    public List<string> usedCommandsList;

    // Update is called once per frame
    void Update()
    {

    }

    public void InterpretCommand()
    {
        List<ActorCommand> commands = new List<ActorCommand>();

        if (usedCommandsList.Count == 0)
            return;

        for (int i = 0; i <= usedCommandsList.Count; i++)
        {
            if (usedCommandsList[i].Length == 0)
                return;

            string commandName = usedCommandsList[i].Substring(0, usedCommandsList[i].IndexOf('('));

            if (usedCommandsList.Find(x => x == usedCommandsList[i]) == null) // command not found
                return;

            switch (commandName)
            {
                case "move":
                    {
                        string param1 = usedCommandsList[i].Substring(usedCommandsList[i].IndexOf('(')+1, usedCommandsList[i].IndexOf(','));
                        //check direction
                        directionType dir = Enum.Parse(typeof(directionType), param1);
                       // if (Enum.TryParse(typeof(directionType), param1, out dir))
                      //      MessageBox.Show("Defined");  // Defined for "New_Born, 1, 4 , 8, 12"
                        //check number

                        //tmp
                        MoveCommand move = new MoveCommand(null, 2, "f", 4);
                        commands.Add(move);
                        break;
                    }
                case "interact":
                    {

                        break;
                    }
                case "possess":
                    {
                        break;
                    }
            }
        }
        GameManager.Instance.ExecuteCommands(commands);
    }
}
