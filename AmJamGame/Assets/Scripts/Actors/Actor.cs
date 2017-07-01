using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [TagSelector]
    public string[] blocks = new string[] { };

    [TagSelector]
    public string[] hazards = new string[] { };

    public int movementStep = 1;

    protected int tileSize = 1;

    private void Awake()
    {
        GameManager.Instance.RegisterActor(this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.UregisterActor(this);
    }

    public abstract string PossessOverlappedActor();

    public abstract string MakeInteraction();

    public virtual string Move(directionType direction)
    {
        var newPosition = transform.localPosition;

        switch (direction)
        {
            case directionType.up:
                newPosition.y += tileSize * movementStep;
                break;
            case directionType.down:
                newPosition.y -= tileSize * movementStep;
                break;
            case directionType.left:
                newPosition.x -= tileSize * movementStep;
                break;
            case directionType.right:
                newPosition.x += tileSize * movementStep;
                break;
        }

        string result = ValidatePosition((int)newPosition.x, (int)newPosition.y);

        if (string.IsNullOrEmpty(result))
        {
            transform.localPosition = newPosition;
        }

        return result;
    }

    public virtual string ValidatePosition(int x, int y)
    {
        var tile = GameManager.Instance.GetTileAtPosition(x, y);

        for(int i = 0; i < hazards.Length; i++)
        {
            if (tile.tag == hazards[i])
                return "Killed by: " + tile.tileName;
        }

        for (int i = 0; i < blocks.Length; i++)
        {
            if (tile.tag == blocks[i])
                return "Blocked by: " + tile.tileName;
        }

        return string.Empty;
    }
}
