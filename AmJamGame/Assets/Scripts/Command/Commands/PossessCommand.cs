using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessCommand : ActorCommand
{
    public PossessCommand(Actor actor, int lineNumber)
            : base("PossessCommand", actor, lineNumber)
    { }

    public override void Execute()
    {
        base.Execute();
        ExecutionResult = actor.PossessOverlappedActor();

        if (string.IsNullOrEmpty(ExecutionResult))
            ExecutionProgress = EExecutionProgress.SUCCESS;
        else
            ExecutionProgress = EExecutionProgress.FAILED;

        Abort();
    }
}
