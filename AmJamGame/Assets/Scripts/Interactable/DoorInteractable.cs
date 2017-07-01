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

        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (var rendr in renderers)
            rendr.enabled = false;
    }

    public override void ResetInteractiable()
    {
        base.ResetInteractiable();
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (var rendr in renderers)
            rendr.enabled = true;
    }
}
