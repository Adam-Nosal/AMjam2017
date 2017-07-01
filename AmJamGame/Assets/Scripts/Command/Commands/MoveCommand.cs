using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ActorCommand
{
    private directionType direction;
    private int iterations;

    public MoveCommand(Actor actor, int lineNumber, directionType direction, int iterations)
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
        ExecutionProgress = EExecutionProgress.SUCCESS;
        Abort();
        
    }
}
