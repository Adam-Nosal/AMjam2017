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

    void Awake()
    {
        commandsManager = new CommandsManager(CommandsManager.UpdateMethod.MANUAL);
        commandsManager.AddContext(new BaseContext("ActorsContext"));
    }

    private IEnumerator Start()
    {
        StartCoroutine(CustomUpdate());

        yield return new WaitForSeconds(3f);

        var l = new List<ActorCommand>();
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

        ExecuteCommands(l);
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

    private IEnumerator CustomUpdate()
    {
        if (isCodeRunning)
            commandsManager.UpdateQueue();

        yield return new WaitForSeconds(1f);

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
        }

        possessionHistory.Add(actor);
        GetPossessedActor().transform.localPosition = new Vector3(possessedActor.transform.localPosition.x, possessedActor.transform.localPosition.y, -1);
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
        Debug.Log("Level complete!");
    }

    public void ResetGame()
    {
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ResetGame();
    }
}
