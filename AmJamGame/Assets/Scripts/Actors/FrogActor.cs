using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogActor : Actor
{
    public override string Move(directionType direction)
    {

        WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.StepFrog);


        return base.Move(direction);
    }


}
