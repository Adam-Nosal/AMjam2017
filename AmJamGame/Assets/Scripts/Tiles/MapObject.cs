using System;
using UnityEngine;
using System.Collections.Generic;

public class MapObject
{
    public GameObject[,] gos;
    public Dictionary<int, List<GameObject>> rooms;

    public MapObject(int w, int h) 
    {
        gos = new GameObject[w, h];
        rooms = new Dictionary<int, List<GameObject>>();
    }

    public void AddToRoom(int roomCode, GameObject go) 
    {
        if(!rooms.ContainsKey(roomCode))
        {
            rooms[roomCode] = new List<GameObject>();
        }
        rooms[roomCode].Add(go);
    }
}

