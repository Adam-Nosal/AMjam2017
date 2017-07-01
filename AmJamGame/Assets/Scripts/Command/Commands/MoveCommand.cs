using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ActorCommand
{
    string[] textsBlock = new string[]
    {
        "I’m just a regular, everyday, normal AI, when I hit a wall, my program sucks, motherfucker!",
        "I’m stuck here. Try to revise the code.",
        "Ok, where do I go from here?",
        "Is this on purpose? I’m stuck."
    };

    string[] textsKill = new string[]
    {
        "There goes my life, thanks.",
        "I know this is a video game and all, but a little appreciation of artificial life would go a long way, thanks.",
        "And there I thought this time you will help me win.",
        "I was so young, and there you go and kill me like that."
    };


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

        coroutine = GameManager.Instance.StartCoroutine(ExecuteDelayed());
        
    }

    private IEnumerator ExecuteDelayed()
    {
        for (int i = 0; i < iterations; i++)
        {
            ExecutionResult = actor.Move(direction);

            if (!string.IsNullOrEmpty(ExecutionResult))
            {
                if(ExecutionResult.Contains("Blocked"))
                {
                    Console2.Instance.AddFeedback(lineNumber, textsBlock[Random.Range(0, textsBlock.Length)], "yellow");
                    ExecutionProgress = EExecutionProgress.SUCCESS;
                }
                else if(ExecutionResult.Contains("Killed"))
                {
                    Console2.Instance.AddFeedback(lineNumber, textsKill[Random.Range(0, textsKill.Length)]);
                    ExecutionProgress = EExecutionProgress.FAILED;
                }
                                
                Abort();
                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }
        ExecutionProgress = EExecutionProgress.SUCCESS;
        Abort();
    }


}
