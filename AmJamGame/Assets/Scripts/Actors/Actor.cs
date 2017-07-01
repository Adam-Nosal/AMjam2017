using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [TagSelector]
    public string[] blocks = new string[] { };

    [TagSelector]
    public string[] hazards = new string[] { };

    public abstract string Move(string direction);

    public abstract string PossessOverlappedActor();

    public abstract string MakeInteraction();
}
