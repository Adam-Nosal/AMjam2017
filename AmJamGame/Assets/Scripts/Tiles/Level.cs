using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : Singleton<Level>
{
    private static string[] levels = { "1", "1", "3", "4", "5" };
    private MapObject mapObject;
    [SerializeField]
    private GameObject InteracibleParent;
    [SerializeField]
    private List<GameObject> InteractibleObjects;
    [SerializeField]
    private GameObject ActorParent;
    [SerializeField]
    private List<GameObject> ActorObjects;
    [HideInInspector]
    public string levelToOpen;

    private void Awake()
    {
        if (!string.IsNullOrEmpty(levelToOpen))
            LoadLevelByName(levelToOpen);
    }

    public void LoadLevelById(int o)
    {
        LoadLevelByName(levels[o]);
  

    }

    public void LoadLevelByName(string name)
    {
        mapObject = TilemapLoader.LoadMapFromFile(name, transform, InteracibleParent.transform, ActorParent.transform, WorldManager.Instance.GetTagHelper());
        InteractibleObjects = new List<GameObject>();
        int children = InteracibleParent.transform.childCount;
        for (int i = 0; i < children; ++i)
            InteractibleObjects.Add(transform.GetChild(i).gameObject);

        ActorObjects = new List<GameObject>();
        children = ActorParent.transform.childCount;
        for (int i = 0; i < children; ++i)
            ActorObjects.Add(transform.GetChild(i).gameObject);
    }

    public void ReplaceInRoom(int room, GameObject old, GameObject fresh)
    {
        mapObject.rooms[room].Remove(old);
        mapObject.rooms[room].Add(fresh);
    }

    public void SpawnEnemies()
    {
        foreach (KeyValuePair<int, List<GameObject>> room in mapObject.rooms)
        {
            if (room.Key == 0) continue;
            Dictionary<GameObject, List<GameObject>> edges = new Dictionary<GameObject, List<GameObject>>();
            foreach (GameObject o in room.Value)
            {
                foreach (GameObject q in room.Value)
                {
                    if (!edges.ContainsKey(o))
                    {
                        edges[o] = new List<GameObject>();
                    }
                    if (q != o && Vector3.Distance(o.transform.position, q.transform.position) < 1.2f)
                    {
                        edges[o].Add(q);
                    }
                }
            }

        }
    }


}