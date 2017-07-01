using System;
using System.Collections;
using UnityEngine;

public class Replacable : Interactable
{
    public GameObject nextState;

    protected override void Interact()
    {
        base.Interact();
        StartCoroutine(ReplaceAfterDelay());
    }

    private IEnumerator ReplaceAfterDelay()
    {
        int roomCode = base.roomCode;
        Vector3 position = transform.position;
        Transform parent = transform.parent;
        GameObject next = nextState;
        GameObject go = Instantiate(next, parent);
        go.transform.position = position - new Vector3(0, 0, 1);
        Level.Instance.ReplaceInRoom(roomCode, gameObject, go);
        
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        go.transform.position = position;
    }
}
