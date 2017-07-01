using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyActor : Actor
{
    public override string Move(directionType direction)
    {
        var interactable = GameManager.Instance.GetInteractableAtPosition((int)transform.localPosition.x, (int)transform.localPosition.y);

        if (interactable != null && interactable.tag == "DoorClosed")
            return string.Empty;

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
}
