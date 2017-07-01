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

    public ActorCommand(string name, Actor actor, int lineNumber)
            : base(name, "ActorsContext")
    {
        this.actor = actor;
        this.lineNumber = lineNumber;
    }

    public override void Execute()
    {
        ExecutionProgress = EExecutionProgress.INPROGRESS;
        base.Execute();
    }
}
