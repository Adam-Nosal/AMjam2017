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

    public override void SetPossessed(bool possess)
    {
        base.SetPossessed(possess);
       // WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.Frog);
    }

    public override void PrintPossess()
    {
        int index = UnityEngine.Random.Range(0, textsPossess.Length);
        WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.Frog, index);
        WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.Possess);
        Console2.Instance.AddFeedback(-1, textsPossess[index], "white");
    }
}
