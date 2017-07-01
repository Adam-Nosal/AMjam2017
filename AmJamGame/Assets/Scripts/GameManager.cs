using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CommandsManager commandsManager;
    public bool isCodeRunning = false;

    void Awake()
    {
        commandsManager = new CommandsManager(CommandsManager.UpdateMethod.MANUAL);
    }

    public void ExecuteCommands(List<ActorCommand> commands)
    {
        commandsManager.Clear();

        foreach (var command in commands)
            commandsManager.AddToQueue(command);

        isCodeRunning = true;
    }

    public void StopExecution()
    {
        isCodeRunning = false;
    }

    private void Update()
    {
        if (isCodeRunning)
            commandsManager.UpdateQueue();
    }
}
