using System;
using UnityEngine;
using System.IO;
using System.Linq;

public class TilemapLoader
{
    public static MapObject LoadMapFromFile(string file,
        Transform parent)
	{
        string json = Resources.Load<TextAsset>(file).text;
		Map map = JsonUtility.FromJson<Map>(json);

        int max = map.tilesets[1].firstgid;

        MapObject result = new MapObject(map.width, map.height);

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                int i = y * map.width + x;
                int idx = map.layers[0].data[i];
                int roomCode = map.layers[1].data[i];
                GameObject go = result.gos[x, y] = GameObject.Instantiate(Tileset.Instance.GetTile(idx), parent);


                go.transform.localPosition = new Vector2(x - map.width / 2, -y + map.height / 2);
                if(roomCode == 0)
                {
                    result.AddToRoom(0, go);
                }
                else
                {
                    roomCode = roomCode - max + 1;
                    Interactable interactable = go.GetComponent<Interactable>();
                    if(interactable != null)
                    {
                        interactable.InRoom(roomCode);
                    }
                    result.AddToRoom(roomCode, go);
                }
            }
        }

        return result;
	}
}
