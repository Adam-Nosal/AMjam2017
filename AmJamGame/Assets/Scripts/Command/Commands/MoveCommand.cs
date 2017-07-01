using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ActorCommand
{
    public MoveCommand(Actor actor)
            : base("MoveCommand", actor)
    {}

    public override void Execute()
    {
        base.Execute();
    }
}
