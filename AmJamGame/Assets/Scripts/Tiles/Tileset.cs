using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Tileset : Singleton<Tileset>
{
    public GameObject[] tiles;

    public GameObject GetTile(int id)
    {
        try
        {
            return tiles[id - 1];

        }
        catch
        {
            return tiles[0];
        }
    }



    // Toolset 
      [ContextMenu("TilesetTools/CreatePrefabsFromTilemap")]
    public void GenerateTilePrefabs()
    {
        string spriteSheet = AssetDatabase.GetAssetPath(TileMap);
        Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(spriteSheet).OfType<Sprite>().ToArray();
        GameObject finalPrefab = new GameObject();
        finalPrefab.AddComponent<BoxCollider>();
        finalPrefab.AddComponent<Interactable>();
        SpriteRenderer sRenderer = finalPrefab.AddComponent<SpriteRenderer>();
        
        foreach (Sprite t in sprites)
       {
            
           sRenderer.sprite = t;
            Object prefab = PrefabUtility.CreateEmptyPrefab(PrefabsPath + t.name + ".prefab");
           PrefabUtility.ReplacePrefab(finalPrefab, prefab, ReplacePrefabOptions.ConnectToPrefab);
           
       }
       // GameObject.Destroy(finalPrefab);

   }

    [HideInInspector] public  Texture TileMap;
    [HideInInspector] public string PrefabsPath;
}

