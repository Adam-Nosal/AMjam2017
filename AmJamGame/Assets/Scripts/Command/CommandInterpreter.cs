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

    public List<string> usedCommandsList = new List<string>();

    // Update is called once per frame
    void Update()
    {

    }

    public void InterpretCommands()
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
                        int bracket = usedCommandsList[i].IndexOf('(');
                        int comma = usedCommandsList[i].IndexOf(',');
                        string param1 = usedCommandsList[i].Substring(bracket+1, comma - (bracket + 1));
                        directionType dir;
                        //check direction
                        try {
                            dir = (directionType)Enum.Parse(typeof(directionType), param1);
                        }
                        catch(Exception e)
                        {
                            return;
                        }

                        //check number
                        int iterations;
                        int.TryParse(usedCommandsList[i].Substring(comma + 1, usedCommandsList[i].IndexOf(')') - (bracket + 1)), out iterations);
                        if (iterations == 0)
                            return;    

                        //tmp
                        MoveCommand move = new MoveCommand(null, i, "f", iterations);
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
