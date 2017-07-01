using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorCommand : BaseCommand
{
    public enum EExecutionProgress
    {
        NONE,
        INPROGRESS,
        SUCCESS,
        FAILED
    }

    public EExecutionProgress ExecutionProgress { get; protected set; }
    public string ExecutionResult { get; protected set; }


    public Actor actor;
    public int lineNumber;

    protected Coroutine coroutine;

    public ActorCommand(string name, Actor actor, int lineNumber)
            : base(name, "ActorsContext")
    {
        this.actor = actor;
        this.lineNumber = lineNumber;
    }

    public override void Execute()
    {
        actor = GameManager.Instance.GetPossessedActor();

        ExecutionProgress = EExecutionProgress.INPROGRESS;
        base.Execute();
    }

    public override void Abort()
    {
        if(coroutine != null)
            GameManager.Instance.StopCoroutine(coroutine);

        Debug.Log(string.Format("Command {0} complete with result: {1}: {2}", Name, ExecutionProgress, ExecutionResult));
        base.Abort();
    }
}
