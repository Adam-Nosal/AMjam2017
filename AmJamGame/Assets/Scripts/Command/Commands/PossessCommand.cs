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
        {
            GameManager.Instance.GetPossessedActor().PrintPossess();
            ExecutionProgress = EExecutionProgress.SUCCESS;
        }
        else
        {
            Console2.Instance.AddFeedback(lineNumber, "There's nothing to possess here...", "red");
            ExecutionProgress = EExecutionProgress.FAILED;
        }            

        Abort();
        yield return new WaitForSeconds(1f);
    }
}
