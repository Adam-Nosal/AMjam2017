using System;
using UnityEngine;

public class InteractBehaviour : ActorBehaviour
{
    public class InteractBehaviourExecutionInfo : BehaviourExecutionInfo
    {
        public InteractBehaviourExecutionInfo(Actor target, Action<BehaviourExecutionResult> OnComplete) : base(target, OnComplete)
        {
        }
    }

    public override string BehaviourName { get { return "interact"; } }

    [TagSelector][SerializeField]
    [Tooltip("Tags of game objects that target actor can interact with.")]
    private string[] interactables = new string[] { };

    public override void Execute(BehaviourExecutionInfo info)
    {
        var exeInfo = info as InteractBehaviourExecutionInfo;
        //TODO: get info about tile where actor stands.
    }

    public override EParseParametersStatus ParseParameters(ref BehaviourExecutionInfo executionInfo, params string[] parameters)
    {
        if (parameters.Length > 1)
            return EParseParametersStatus.TooManyParams;

        return EParseParametersStatus.Success;
    }
}

