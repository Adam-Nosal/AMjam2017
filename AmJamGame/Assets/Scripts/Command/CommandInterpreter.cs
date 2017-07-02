using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum directionType { up, down, left, right }

public class CommandInterpreter : Singleton<CommandInterpreter>
{
    Actor actor;
    List<string> commandsList = new List<string>()
        {
            "mov",
            "use",
            "pos",
            "winner"
        };
    // Use this for initialization
    void Start()
    {
        //commandsList = new List<string>()
        //{
        //    "move",
        //    "interact",
        //    "possess"
        //};
    }

    public List<string> usedCommandsList = new List<string>();

    // Update is called once per frame
    void Update()
    {
       
    }

    public void InterpretCommands()
    {
        List<ActorCommand> commands = new List<ActorCommand>();      


        for (int i = 0; i < usedCommandsList.Count; i++)
        {
            if (usedCommandsList[i].Length == 0 || usedCommandsList[i].Replace(" ", string.Empty).Length == 0)
            {
                // empty
            }

            if (usedCommandsList[i].IndexOf('(') < 0 )
            {
                Console2.Instance.AddFeedback(i,  TextManager.Instance.GetBacketsText());
                return;
            }

            string commandName = usedCommandsList[i].Substring(0, usedCommandsList[i].IndexOf('('));

            if (commandsList.Find(x => x == commandName) == null) // command not found
            {
                Console2.Instance.AddFeedback(i, TextManager.Instance.GetWrongCommandText());
                return;
            }

            switch (commandName)
            {
                case "mov":
                    {
                        int bracket = usedCommandsList[i].IndexOf('(');
                        int comma = usedCommandsList[i].IndexOf(',');

                        if(comma<0)
                        {
                            Console2.Instance.AddFeedback(i, TextManager.Instance.GetWrongNumerText());
                            return;
                        }

                        string param1 = usedCommandsList[i].Substring(bracket+1, comma - (bracket + 1)).Replace(" ", string.Empty);
                        directionType dir;

                        //if (param1==directionType.down.ToString() || param1 == directionType.up.ToString() || param1 == directionType.left.ToString() || param1 == directionType.right.ToString())
                        //    dir = (directionType)Enum.Parse(typeof(directionType), param1);
                        //else
                        //{
                        //    Console2.Instance.AddFeedback(i, TextManager.Instance.GetWrongFirstText());
                        //    return;
                        //}

                        switch (param1)
                        {
                            case "l":
                            case "left":
                                dir = directionType.left;
                                break;
                            case "r":
                            case "right":
                                dir = directionType.right;
                                break;
                            case "u":
                            case "up":
                                dir = directionType.up;
                                break;
                            case "d":
                            case "down":
                                dir = directionType.down;
                                break;
                            default:
                                Console2.Instance.AddFeedback(i, TextManager.Instance.GetWrongFirstText());
                                return;
                        }

                       

                        //check number
                        int iterations;
                        int.TryParse(usedCommandsList[i].Substring(comma + 1, usedCommandsList[i].IndexOf(')') - (comma + 1)).Replace(" ", string.Empty), out iterations);
                        if (iterations == 0)
                        {
                            Console2.Instance.AddFeedback(i, TextManager.Instance.GetWrongSecondText());
                            return;
                        }  
                        
                        MoveCommand move = new MoveCommand(actor, i, dir, iterations);
                        commands.Add(move);
                        break;
                    }
                case "use":
                    {
                        InteractCommand move = new InteractCommand(actor, i);
                        commands.Add(move);
                        break;
                    }
                case "pos":
                    {
                        PossessCommand move = new PossessCommand(actor, i);
                        commands.Add(move);
                        break;
                    }
                case "winner":
                    {
                        GameManager.Instance.CompleteLevel();
                        break;
                    }
            }

            if (usedCommandsList[i].IndexOf(");") != usedCommandsList[i].Length - 2)
            {
                Console2.Instance.AddFeedback(i, TextManager.Instance.GetSemicolonText());
                return;
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
