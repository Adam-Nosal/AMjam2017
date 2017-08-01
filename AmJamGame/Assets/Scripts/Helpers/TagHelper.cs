using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "TagHelper")]
public class TagHelper : ScriptableObject {

    [SerializeField] private List<string> InteractibleTags;
    [SerializeField] private List<string> ActorTags;

    public bool CheckIfTagIsInteractible(string tag)
    {
        if (InteractibleTags.Contains(tag))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIfTagIsActor(string tag)
    {
        if (ActorTags.Contains(tag))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
