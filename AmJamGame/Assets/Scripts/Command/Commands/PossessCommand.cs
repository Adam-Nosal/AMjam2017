using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessCommand : ActorCommand
{
    public PossessCommand(Actor actor)
            : base("PossessCommand", actor)
    { }

    public override void Execute()
    {
        base.Execute();
    }
}
