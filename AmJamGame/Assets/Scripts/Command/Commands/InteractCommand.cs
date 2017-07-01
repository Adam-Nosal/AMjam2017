using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCommand : ActorCommand
{
    public InteractCommand(Actor actor, int lineNumber)
            : base("InteractCommand", actor, lineNumber)
    { }

    public override void Execute()
    {
        base.Execute();
        ExecutionResult = actor.MakeInteraction();

        if (string.IsNullOrEmpty(ExecutionResult))
            ExecutionProgress = EExecutionProgress.SUCCESS;
        else
            ExecutionProgress = EExecutionProgress.FAILED;

        Abort();
    }
}
