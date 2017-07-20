using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public abstract class ActorBehaviour : MonoBehaviour
{
    public enum EParseParametersStatus
    {
        Success,
        Fail,
        TooManyParams,
        NotEnoughParams,
        WrongParams
    }

    [Tooltip("If true, Behaviour will be executed on every tick of program automatically.")]
    public bool autoExecute;
    [Tooltip("If true, Behaviour can't be executed by player.")]
    public bool hiddenForPlayer;

    public Actor Owner { get; private set; }
    public abstract string BehaviourName { get; }

    private void Awake()
    {
        Owner = GetComponent<Actor>();
    }

    public abstract void Execute(BehaviourExecutionInfo info);
    public abstract EParseParametersStatus ParseParameters(ref BehaviourExecutionInfo executionInfo, params string[] parameters);
}
