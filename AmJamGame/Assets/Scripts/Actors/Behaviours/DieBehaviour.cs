using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBehaviour : ActorBehaviour
{
    public class DieBehaviourExecutionInfo : BehaviourExecutionInfo
    {
        public bool ignoreHazards;

        public DieBehaviourExecutionInfo(Actor target, Action<BehaviourExecutionResult> OnComplete, bool ignoreHazards = false) : base(target, OnComplete)
        {
            this.ignoreHazards = false;
        }
    }

    public override string BehaviourName { get { return "die"; } }

    [TagSelector][SerializeField]
    [Tooltip("Tags of game objects that kills target actor.")]
    private string[] hazards = new string[] { };

    public override void Execute(BehaviourExecutionInfo info)
    {
        var exeInfo = info as DieBehaviourExecutionInfo;
        //TODO: get info about tile where actor stands.
    }

    public override EParseParametersStatus ParseParameters(ref BehaviourExecutionInfo executionInfo, params string[] parameters)
    {
        if (parameters.Length > 1)
            return EParseParametersStatus.TooManyParams;
        
        if(parameters.Length == 1)
        {
            bool ignoreHazards;
            if (bool.TryParse(parameters[0], out ignoreHazards))
                (executionInfo as DieBehaviourExecutionInfo).ignoreHazards = ignoreHazards;
            else
                return EParseParametersStatus.WrongParams;
        }

        return EParseParametersStatus.Success;
    }
}
