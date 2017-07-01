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
}
