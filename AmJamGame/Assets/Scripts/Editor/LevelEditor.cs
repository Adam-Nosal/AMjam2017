using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    string levelToOpen = "untitled";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Level level = (Level)target;
        levelToOpen = EditorGUILayout.TextField("Map Name", levelToOpen);
        if(GUILayout.Button("Load From File"))
        {
            level.LoadLevelByName(levelToOpen);
        }
        if(GUILayout.Button("Clean children"))
        {
            var children = new List<GameObject>();
            foreach (Transform child in level.transform) children.Add(child.gameObject);
            children.ForEach(child => DestroyImmediate(child));
        }
    }
}
