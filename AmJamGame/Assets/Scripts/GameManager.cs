using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CommandsManager commandsManager;
    public bool isCodeRunning = false;

    public List<Actor> possessionHistory;

    public List<Actor> actors = new List<Actor>();
    public List<Tile> tiles = new List<Tile>();
    public List<InteractableObject> interactables = new List<InteractableObject>();

    public JumpCamera2D camera2D;

    private string[] textsb = new string[]
    {
        "There, was that so hard?",
        "Awesome, I made it! Erm, I mean, we made it.",
        "Treasure collected! You helped some, thanks.",
        "I made it, and it was all on your own!"
    };

    private int commandsNum = 0;
    
    void Awake()
    {
        commandsManager = new CommandsManager(CommandsManager.UpdateMethod.MANUAL);
        commandsManager.AddContext(new BaseContext("ActorsContext"));

        if (camera2D == null)
        {
            camera2D = Camera.main.gameObject.GetComponent<JumpCamera2D>();
            if (camera2D == null)
            {
                Debug.LogError("There is no JumpCamera in the scene");
            }
        }
    }

    private IEnumerator Start()
    {
        GetPossessedActor().SetPossessed(true);
        StartCoroutine(CustomUpdate());
      //  WorldManager.Instance.soundManager.PlayAmbient();
        yield return new WaitForSeconds(3f);

        /*var l = new List<ActorCommand>();
        l.Add(new MoveCommand(GetPossessedActor(), 0, directionType.right, 2));
        l.Add(new PossessCommand(GetPossessedActor(), 0));
        l.Add(new MoveCommand(GetPossessedActor(), 0, directionType.right, 1));
        l.Add(new MoveCommand(GetPossessedActor(), 0, directionType.up, 1));
        l.Add(new InteractCommand(GetPossessedActor(), 0));
        l.Add(new MoveCommand(GetPossessedActor(), 0, directionType.right, 1));
        l.Add(new MoveCommand(GetPossessedActor(), 0, directionType.up, 3));
        l.Add(new MoveCommand(GetPossessedActor(), 0, directionType.left, 5));
        l.Add(new PossessCommand(GetPossessedActor(), 0));
        l.Add(new MoveCommand(GetPossessedActor(), 0, directionType.up, 1));
        l.Add(new MoveCommand(GetPossessedActor(), 0, directionType.right, 1));

        ExecuteCommands(l);*/
    }

    public void ExecuteCommands(List<ActorCommand> commands)
    {
        commandsNum = 0;
        ResetGame();

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
        if(actorCommand.ExecutionProgress == ActorCommand.EExecutionProgress.FAILED)
        {
            commandsNum = 0;
            commandsManager.Clear();
            Failed();

            isCodeRunning = false;

            return;
        }

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

    public void RegisterActor(Actor actor)
    {
        actors.Add(actor);
    }

    public void UregisterActor(Actor actor)
    {
        if (actors.Contains(actor))
            actors.Remove(actor);
    }

    public void RegisterTile(Tile tile)
    {
        tiles.Add(tile);
    }

    public void RegisterInteractable(InteractableObject interactable)
    {
        interactables.Add(interactable);
    }

    public void UnregisterInteractable(InteractableObject interactable)
    {
        if (interactables.Contains(interactable))
            interactables.Remove(interactable);
    }

    public void BackHistory()
    {
        if(possessionHistory.Count > 0)
            possessionHistory.RemoveAt(possessionHistory.Count - 1);
    }

    public void UpdatedPossessedActor(Actor actor)
    {
        var possessedActor = GetPossessedActor();
        if (possessedActor != null)
        {
            possessedActor.transform.localPosition = new Vector3(possessedActor.transform.localPosition.x, possessedActor.transform.localPosition.y, 0);
            possessedActor.SetPossessed(false);
        }

        possessionHistory.Add(actor);
        GetPossessedActor().transform.localPosition = new Vector3(possessedActor.transform.localPosition.x, possessedActor.transform.localPosition.y, -1);
        GetPossessedActor().SetPossessed(true);
        camera2D.SetNewTarget(GetPossessedActor().gameObject);
    }

    public Actor GetPossessedActor()
    {
        if (possessionHistory.Count == 0)
            return null;

        return possessionHistory[possessionHistory.Count - 1];
    }

    public Tile GetTileAtPosition(int x, int y)
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].transform.localPosition.x == x && tiles[i].transform.localPosition.y == y)
                return tiles[i];
        }

        return null;
    }

    public Actor GetActorAtPosition(int x, int y, Actor exclude)
    {
        for (int i = 0; i < actors.Count; i++)
        {
            if (actors[i] != exclude && (int)actors[i].transform.localPosition.x == x && (int)actors[i].transform.localPosition.y == y)
                return actors[i];
        }

        return null;
    }

    public InteractableObject GetInteractableAtPosition(int x, int y)
    {
        for (int i = 0; i < interactables.Count; i++)
        {
            if (interactables[i].transform.localPosition.x == x && interactables[i].transform.localPosition.y == y)
                return interactables[i];
        }

        return null;
    }

    public void CompleteLevel()
    {
Console2.Instance.AddFeedback(-1, textsb[Random.Range(0, textsb.Length)], "green"); WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.Success);        Debug.Log("Level complete!");
        WorldManager.Instance.LoadNextLevel();

    }
    public void Failed()
    {
        WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.Fail);

        Debug.Log("Failed");
    }

    public void ResetGame()
    {
        WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.RunProgram);
        GetPossessedActor().SetPossessed(false);
        isCodeRunning = false;
        commandsNum = 0;

        var act = possessionHistory[0];
        possessionHistory = new List<Actor>();
        possessionHistory.Add(act);

        commandsManager.Clear();

        var allActors = GetComponentsInChildren<Actor>();
        var allInteractiables = GetComponentsInChildren<InteractableObject>();

        for(int i = 0; i < allActors.Length; i++)
        {
            UregisterActor(allActors[i]);
            allActors[i].ResetActor();
        }

        for (int i = 0; i < allInteractiables.Length; i++)
        {
            UnregisterInteractable(allInteractiables[i]);
            allInteractiables[i].ResetInteractiable();
        }
        GetPossessedActor().SetPossessed(true);
        camera2D.SetNewTarget(GetPossessedActor().gameObject);
    }
}
