using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : InteractableObject
{
    public override void Interact(Actor actor)
    {
        base.Interact(actor);

        actor.Kill();

        GameManager.Instance.UnregisterInteractable(this);

        gameObject.SetActive(false);
    }
}
