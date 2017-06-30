using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Tileset))]
public class TilesetEditor : Editor
{
    string prefabsPath = "Assets/Resources/Tiles/";
    Texture TileMap;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Tileset TileSet = (Tileset)target;
        prefabsPath = EditorGUILayout.TextField("Prefabs Path", prefabsPath);

        if (TileSet.TileMap != null)
            TileMap = TileSet.TileMap;

        TileMap = (Texture)EditorGUILayout.ObjectField("TileMap", TileMap, typeof(Texture2D), false);
        TileSet.PrefabsPath = prefabsPath;
        TileSet.TileMap = TileMap;

        if (GUILayout.Button("GeneratePrefabs"))
        {
            TileSet.GenerateTilePrefabs();
        }

    }
}
