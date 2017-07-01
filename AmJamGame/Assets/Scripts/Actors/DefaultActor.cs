using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultActor : Actor
{

    public override string Move(directionType direction)
    {

        WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.StepHuman);


        return base.Move(direction);
    }
}
