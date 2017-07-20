using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : ActorBehaviour
{
    public class MoveBehaviourExecutionInfo : BehaviourExecutionInfo
    {
        public EMoveDirection direction;

        public MoveBehaviourExecutionInfo(Actor target, Action<BehaviourExecutionResult> OnComplete, EMoveDirection direction = EMoveDirection.None) : base(target, OnComplete)
        {
            this.direction = direction;
        }
    }

    public enum EMoveDirection
    {
        None,
        Up,
        Right,
        Down,
        Left
    }

    public override string BehaviourName { get { return "move"; } }

    [TagSelector][SerializeField]
    [Tooltip("Tags of game objects that blocks target actor.")]
    private string[] blockers = new string[] { };

    public override void Execute(BehaviourExecutionInfo info)
    {
        var exeInfo = info as MoveBehaviourExecutionInfo;
        var movementVector = GetDirectionVector(exeInfo.direction);
        //TODO: get info about tile where actor stands.
    }

    public override EParseParametersStatus ParseParameters(ref BehaviourExecutionInfo executionInfo, params string[] parameters)
    {
        if (parameters.Length < 1)
            return EParseParametersStatus.NotEnoughParams;
        else if (parameters.Length > 1)
            return EParseParametersStatus.TooManyParams;

        var exeInfo = executionInfo as MoveBehaviourExecutionInfo;
        exeInfo.direction = GetDirectionByParam(parameters[0]);

        if(exeInfo.direction == EMoveDirection.None)
            return EParseParametersStatus.WrongParams;

        return EParseParametersStatus.Success;
    }

    private static Vector2 GetDirectionVector(EMoveDirection direction)
    {
        switch(direction)
        {
            case EMoveDirection.Up:
                return Vector2.up;
            case EMoveDirection.Right:
                return Vector2.right;
            case EMoveDirection.Down:
                return Vector2.down;
            case EMoveDirection.Left:
                return Vector2.left;
            default:
                return Vector2.zero;
        }
    }

    private static EMoveDirection GetDirectionByParam(string direction)
    {
        switch (direction)
        {
            case "u":
                return EMoveDirection.Up;
            case "r":
                return EMoveDirection.Right;
            case "d":
                return EMoveDirection.Down;
            case "l":
                return EMoveDirection.Left;
            default:
                return EMoveDirection.None;
        }
    }
}
