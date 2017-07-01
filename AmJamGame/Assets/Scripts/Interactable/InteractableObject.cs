using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	// Use this for initialization
	void Awake ()
    {
        GameManager.Instance.RegisterInteractable(this);		
	}

    public virtual void Interact(Actor actor)
    {

    }

    public virtual void ResetInteractiable()
    {
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (var rendr in renderers)
            rendr.enabled = true;

        GameManager.Instance.RegisterInteractable(this);
    }
}
