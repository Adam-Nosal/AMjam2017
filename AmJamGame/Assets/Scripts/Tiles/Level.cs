using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : Singleton<Level>
{
    private static string[] levels = { "main" };
    public MapObject mapObject;

    private void Start()
    {
        LoadLevelById(0);SpawnEnemies();
    }

    public void LoadLevelById(int i) 
    {
        LoadLevelByName(levels[i]);
    }

    public void LoadLevelByName(string name)
    {
        mapObject = TilemapLoader.LoadMapFromFile(name, transform);
    }

    public void ReplaceInRoom(int room, GameObject old, GameObject fresh)
    {
        mapObject.rooms[room].Remove(old);
        mapObject.rooms[room].Add(fresh);
    }

    public void SpawnEnemies()
    {
        foreach(KeyValuePair<int,List<GameObject>> room in mapObject.rooms)
        {
            if(room.Key == 0) continue;
            Dictionary<GameObject, List<GameObject>> edges = new Dictionary<GameObject, List<GameObject>>();
            foreach(GameObject o in room.Value)
            {
                foreach(GameObject q in room.Value)
                {
                    if(!edges.ContainsKey(o))
                    {
                        edges[o] = new List<GameObject>();
                    }
                    if(q != o && Vector3.Distance(o.transform.position, q.transform.position) < 1.2f)
                    {                        
                        edges[o].Add(q);
                    }
                }
            }
           
        }
    }

   
}