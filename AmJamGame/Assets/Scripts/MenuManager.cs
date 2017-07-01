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
    
    private int commandsNum = 0;

    void Awake()
    {
        commandsManager = new CommandsManager(CommandsManager.UpdateMethod.MANUAL);
        commandsManager.AddContext(new BaseContext("ActorsContext"));
        
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
        outputText.text = Credits;
    }


    private string Credits = " GameDesign:" + Environment.NewLine + 
        " Michał Sapiński " + Environment.NewLine + Environment.NewLine + 
        "Programming:" + Environment.NewLine + 
        " Agata Chmiel " + Environment.NewLine +
        " Łukasz Górny " + Environment.NewLine + 
        " Adam Nosal " + Environment.NewLine +  Environment.NewLine +
        " SoundDesign: " + Environment.NewLine + 
        " Agnieszka Rumińska";
}
