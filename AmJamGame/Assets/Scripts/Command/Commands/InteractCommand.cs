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

        coroutine = GameManager.Instance.StartCoroutine(ExecuteDelayed());
        
    }

private IEnumerator ExecuteDelayed()
{
        ExecutionResult = actor.MakeInteraction();

        if (string.IsNullOrEmpty(ExecutionResult))
            ExecutionProgress = EExecutionProgress.SUCCESS;
        else
        {
            Console2.Instance.AddFeedback(lineNumber, "There's nothing to use here...", "red");
            ExecutionProgress = EExecutionProgress.FAILED;
        }

        Abort();
        yield return new WaitForSeconds(1f);
    }
}
