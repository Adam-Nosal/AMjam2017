using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ActorBehaviour : MonoBehaviour
{
    [Tooltip("If true, Behaviour will be executed on every tick of program automatically.")]
    public bool autoExecute;

    public abstract string BehaviourName { get; }

    public abstract void Execute(BehaviourExecutionInfo info);
}
