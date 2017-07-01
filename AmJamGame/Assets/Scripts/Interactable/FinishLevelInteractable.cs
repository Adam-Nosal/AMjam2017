using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelInteractable : InteractableObject
{
    public override void Interact(Actor actor)
    {
        base.Interact(actor);

        GameManager.Instance.CompleteLevel();
    }
}
