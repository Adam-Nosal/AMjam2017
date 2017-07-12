using System;
public abstract class BehaviourExecutionInfo
{
    public readonly Actor target;
    public readonly Action<BehaviourExecutionResult> OnComplete;

    public BehaviourExecutionInfo(Actor target, Action<BehaviourExecutionResult> OnComplete)
    {
        this.target = target;
        this.OnComplete = OnComplete;
    }
}
