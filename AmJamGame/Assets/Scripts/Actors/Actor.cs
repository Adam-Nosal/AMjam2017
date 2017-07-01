using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public Actor OverlappedActor;
    public InteractableObject OverlappedInteractableObject;

    public abstract Vector2 MovementBehaviour { get; }

    public abstract string MoveTo(Vector2 position);

    public abstract string PossessOverlappedActor();

    public abstract string MakeInteraction();

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Actor")
            OverlappedActor = collision.gameObject.GetComponent<Actor>();
        else if(collision.gameObject.tag == "Interactable")
            OverlappedInteractableObject = collision.gameObject.GetComponent<InteractableObject>();

    }
}
