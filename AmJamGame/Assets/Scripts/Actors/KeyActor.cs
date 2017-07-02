using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyActor : Actor
{
    public override string Move(directionType direction)
    {
        WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.StepKey);
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

        if (!result.Contains("Blocked"))
        {
            transform.localPosition = newPosition;
        }

        return result;
    }

    public override void SetPossessed(bool possess)
    {
        base.SetPossessed(possess);
       // WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.Key);
    }


    public override void PrintPossess()
    {
        int index = UnityEngine.Random.Range(0, textsPossess.Length);
        WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.Key, index);
        WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.Possess);
        Console2.Instance.AddFeedback(-1, textsPossess[index], "white");
    }

}
