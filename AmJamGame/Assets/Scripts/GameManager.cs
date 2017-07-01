using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CommandsManager commandsManager;
    public bool isCodeRunning = false;

    public List<Actor> actors = new List<Actor>();
    public List<Tile> tiles = new List<Tile>();

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

    public Tile GetTileAtPosition(int x, int y)
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].transform.localPosition.x == x && tiles[i].transform.localPosition.y == y)
                return tiles[i];
        }

        return null;
    }

    public Actor GetActorAtPosition(int x, int y)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (actors[i].transform.localPosition.x == x && actors[i].transform.localPosition.y == y)
                return actors[i];
        }

        return null;
    }
}
