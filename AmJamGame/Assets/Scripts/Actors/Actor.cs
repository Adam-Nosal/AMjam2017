using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    /// <NEW>
    public ActorBehaviour[] Behaviours { get; private set; }

    [Tooltip("Name of the actor.")]
    public string actorName;
    /// </NEW>



    [TagSelector]
    public string[] blocks = new string[] { };

    [TagSelector]
    public string[] hazards = new string[] { };

    [TagSelector]
    public string[] interactables = new string[] { };

    public string[] textsPossess;

    public int movementStep = 1;
    public Vector3 initialPosition;
    protected int tileSize = 1;

    private void Awake()
    {
        /// <NEW>
        var c = GetComponents<ActorBehaviour>();

        /*for(int i = 0; i < components.Length; i++)
        {
            if (components[i].GetType().GetGenericTypeDefinition() == typeof(List<>))
                Behaviours.Add(components[i]);
        }*/

        foreach (var b in c)
        {
            Debug.Log(b.BehaviourName);
        }
        /// </NEW>

      //  initialPosition = transform.localPosition;
       // GameManager.Instance.RegisterActor(this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.UregisterActor(this);
    }

    public virtual string MakeInteraction()
    {
        var interactable = GameManager.Instance.GetInteractableAtPosition((int)transform.localPosition.x, (int)transform.localPosition.y);

        if(interactable == null)
            return "No actor to interact!";

        for (int i = 0; i < interactables.Length; i++)
        {
            if (interactable.tag == interactables[i])
            {
                interactable.Interact(this);
                return string.Empty;
            }                
        }

        return "Cannot interact with: " + interactable.tag;
    }

    public virtual string PossessOverlappedActor()
    {
        var actor = GameManager.Instance.GetActorAtPosition((int)transform.localPosition.x, (int)transform.localPosition.y, this);

        if(actor == null)
        {
            WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.Possess);
            return "No actor to possess!";
        }

        GameManager.Instance.UpdatedPossessedActor(actor);

        return string.Empty;
    }

    public virtual string Move(directionType direction)
    {
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

            if (result.Contains("Win"))
            {
                GameManager.Instance.CompleteLevel();
                return string.Empty;
            }
        }
        else
        {
            WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.Block);
            WorldManager.Instance.ScreenShake();
        }

        return result;
    }

    public virtual void Kill()
    {
        GameManager.Instance.UregisterActor(this);
        GameManager.Instance.BackHistory();
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (var rendr in renderers)
            rendr.enabled = false;
    }

    public virtual string ValidatePosition(int x, int y)
    {
        var tile = GameManager.Instance.GetTileAtPosition(x, y);

        var interactable = GameManager.Instance.GetInteractableAtPosition(x, y);

        if (interactable != null && interactable.tag == "LevelFinish")
        {
            return "Win";
        }

        if (tile == null)
            return "Killed by void";

        if (tile.tag == "LevelFinish")
            return "Win";


            for (int i = 0; i < interactables.Length; i++)
        {
            if (interactables[i] == "Actor" || interactables[i] == "Teleport")
            {
                var actor = GameManager.Instance.GetActorAtPosition(x, y, this);
                if (actor != null)
                    return string.Empty;
            }
        }

        for (int i = 0; i < hazards.Length; i++)
        {
            if (tile.tag == hazards[i])
                return "Killed by: " + tile.tileName;
        }

        for (int i = 0; i < blocks.Length; i++)
        {
            if (tile.tag == blocks[i])
                return "Blocked by: " + tile.tileName;
        }

        return string.Empty;
    }

    public void ResetActor()
    {
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (var rendr in renderers)
            rendr.enabled = true;

        transform.localPosition = initialPosition;
        GameManager.Instance.RegisterActor(this); 
    }

    public virtual void SetPossessed(bool possess)
    {
        if (possess)
        {            
            StartCoroutine("PossessAnim");
        }            
        else
        {
            StopCoroutine("PossessAnim");
            gameObject.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_InvertColors", 0f);
        }
    }

    public virtual void PrintPossess()
    {
        WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.Possess);
        Console2.Instance.AddFeedback(-1, textsPossess[Random.Range(0, textsPossess.Length)], "white");
    }

    public IEnumerator PossessAnim()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_InvertColors", 1f);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponentInChildren<SpriteRenderer>().material.SetFloat("_InvertColors", 0f);

        StartCoroutine("PossessAnim");
    }
}
