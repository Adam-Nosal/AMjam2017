using System;
using UnityEngine;

public class MoveBehaviour : ActorBehaviour
{
    public class MoveBehaviourExecutionInfo : BehaviourExecutionInfo
    {
        public EMoveDirection direction;

        public MoveBehaviourExecutionInfo(EMoveDirection direction, Actor target, Action<BehaviourExecutionResult> OnComplete) : base(target, OnComplete)
        {
            this.direction = direction;
        }
    }

    public enum EMoveDirection
    {
        Up,
        Right,
        Down,
        Left
    }

    public override string BehaviourName { get { return "MoveBehaviour"; } }

    [TagSelector]
    [Tooltip("Tags of game objects that blocks target actor.")]
    public string[] blockers = new string[] { };

    public override void Execute(BehaviourExecutionInfo info)
    {
        var exeInfo = info as MoveBehaviourExecutionInfo;
        var movementVector = GetDirectionVector(exeInfo.direction);
        //TODO: get info about tile where actor stands.
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
}
