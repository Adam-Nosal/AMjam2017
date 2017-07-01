using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ActorCommand
{
    private string direction;
    private int iterations;

    public MoveCommand(Actor actor,int lineNumber, string direction, int iterations)
            : base("MoveCommand", actor, lineNumber)
    {
        this.direction = direction;
        this.iterations = iterations;
    }

    public override void Execute()
    {
        base.Execute();

        for(int i = 0; i < iterations; i++)
        {
            ExecutionResult = actor.Move(direction);

            if (!string.IsNullOrEmpty(ExecutionResult))
            {
                ExecutionProgress = EExecutionProgress.FAILED;
                Abort();
                return;
            }
        }
        
    }
}
