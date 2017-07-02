using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager>
{
    public CommandsManager commandsManager;
    public bool isCodeRunning = false;
    public Text outputText;

    public ConsoleMenu cMenu;
    
    private int commandsNum = 0;

    void Awake()
    {
        commandsManager = new CommandsManager(CommandsManager.UpdateMethod.MANUAL);
        commandsManager.AddContext(new BaseContext("ActorsContext"));
        cMenu = ConsoleMenu.Instance;
    }

    private IEnumerator Start()
    {
        StartCoroutine(CustomUpdate());
        WorldManager.Instance.soundManager.PlayAmbient();
        yield return new WaitForSeconds(3f);
        
    }

    public void ExecuteCommands(List<ActorCommand> commands)
    {
        commandsNum = 0;

        foreach (var command in commands)
        {
            commandsNum += 1;
            commandsManager.AddToQueue(command);
            command.OnExecutionComplete += Command_OnExecutionComplete;
        }

        isCodeRunning = true;
    }

    private void Command_OnExecutionComplete(ICommand command)
    {
        var actorCommand = command as ActorCommand;
        
        commandsNum--;
        if (commandsNum <= 0)
            isCodeRunning = false;
    }

    public void StopExecution()
    {
        isCodeRunning = false;
    }

    private IEnumerator CustomUpdate()
    {
        if (isCodeRunning)
            commandsManager.UpdateQueue();

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(CustomUpdate());
    }
    

    public void PrintCredits()
    {
        cMenu.PrintCredits(Credits); ;
    }


    private string Credits = 
        "GAME DESIGN:" + Environment.NewLine +
        " <color=#29adff>Michał Sapiński</color>" + Environment.NewLine + Environment.NewLine +
        "PROGRAMMING:" + Environment.NewLine +
        " <color=#FF54DCFF>Agata Chmiel " + Environment.NewLine +
        " Łukasz Górny " + Environment.NewLine +
        " Adam Nosal</color>" + Environment.NewLine +  Environment.NewLine +
        "SOUND DESIGN: " + Environment.NewLine +
        " <color=cyan>Agnieszka Rumińska</color>";
}
