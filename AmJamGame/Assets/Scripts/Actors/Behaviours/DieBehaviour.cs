using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBehaviour : ActorBehaviour
{
    public class DieBehaviourExecutionInfo : BehaviourExecutionInfo
    {
        public DieBehaviourExecutionInfo(Actor target, Action<BehaviourExecutionResult> OnComplete) : base(target, OnComplete)
        {
        }
    }

    public override string BehaviourName { get { return "DieBehaviour"; } }

    [TagSelector]
    [Tooltip("Tags of game objects that kills target actor.")]
    public string[] hazards = new string[] { };

    public override void Execute(BehaviourExecutionInfo info)
    {
        var exeInfo = info as DieBehaviourExecutionInfo;
        //TODO: get info about tile where actor stands.
    }
}
