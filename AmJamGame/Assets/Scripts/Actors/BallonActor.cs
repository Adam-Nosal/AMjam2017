using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonActor : Actor
{
    public override string MakeInteraction()
    {
        throw new NotImplementedException();
    }

    public override string Move(directionType direction)
    {
        string result = string.Empty;

        while(result == string.Empty)
        {
            result = base.Move(direction);
        }

        return result;
    }

    public override string PossessOverlappedActor()
    {
        throw new NotImplementedException();
    }
}
