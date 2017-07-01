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

        coroutine = GameManager.Instance.StartCoroutine(ExecuteDelayed());
    }


    private IEnumerator ExecuteDelayed()
    {
        ExecutionResult = actor.PossessOverlappedActor();

        if (string.IsNullOrEmpty(ExecutionResult))
            ExecutionProgress = EExecutionProgress.SUCCESS;
        else
            ExecutionProgress = EExecutionProgress.FAILED;

        Abort();
        yield return new WaitForSeconds(1f);
    }
}
