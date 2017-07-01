using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum directionType { up, down, left, right }

public class CommandInterpreter : Singleton<CommandInterpreter>
{
    Actor actor;
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
        {
            Console.Instance.AddFeedback(0, 0, "Type sth you idiot!");
            return;
        }
        

        for (int i = 0; i < usedCommandsList.Count; i++)
        {
            if (usedCommandsList[i].Length == 0)
            {
                Console.Instance.AddFeedback(i, 0, "Type sth you idiot!");
                usedCommandsList.Clear();
                return;
            }
            if (usedCommandsList[i].IndexOf('(') < 1)
            {
                Console.Instance.AddFeedback(i, 0, "you forgot about brackets again...");
                return;
            }

            string commandName = usedCommandsList[i].Substring(0, usedCommandsList[i].IndexOf('('));

            if (commandsList.Find(x => x == commandName) == null) // command not found
            {
                Console.Instance.AddFeedback(0, 0, "Command not found!");
                return;
            }

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
                        int.TryParse(usedCommandsList[i].Substring(comma + 1, usedCommandsList[i].IndexOf(')') - (comma + 1)), out iterations);
                        if (iterations == 0)
                            return;    

                        //tmp
                        MoveCommand move = new MoveCommand(actor, i, dir, iterations);
                        commands.Add(move);
                        break;
                    }
                case "interact":
                    {
                        InteractCommand move = new InteractCommand(actor, i);
                        commands.Add(move);
                        break;
                    }
                case "possess":
                    {
                        PossessCommand move = new PossessCommand(actor, i);
                        commands.Add(move);
                        break;
                    }
            }
        }

        usedCommandsList.Clear();

        Debug.Log("commands to execute:");
        foreach (var com in commands)
            Debug.Log(com.Name);

        GameManager.Instance.ExecuteCommands(commands);
    }

    void InitialElementsCheck(string command)
    {

    }
}
