using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInteractable : InteractableObject
{
    public Transform teleportTarget;

    public override void Interact(Actor actor)
    {
        base.Interact(actor);

        actor.transform.localPosition = new Vector3(teleportTarget.localPosition.x, teleportTarget.localPosition.y, actor.transform.localPosition.z);
    }
}
