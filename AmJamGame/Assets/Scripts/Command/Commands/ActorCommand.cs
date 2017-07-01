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

    public EExecutionProgress ExecutionProgress;
    public Actor actor;

    public ActorCommand(string name, Actor actor)
            : base(name, "ActorsContext")
    {
        this.actor = actor;
    }

    public override void Execute()
    {
        ExecutionProgress = EExecutionProgress.INPROGRESS;
        base.Execute();
    }
}
