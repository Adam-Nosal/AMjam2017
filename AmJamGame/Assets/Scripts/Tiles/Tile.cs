using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string tileName;

    private void Awake()
    {
        GameManager.Instance.RegisterTile(this);
    }
}
