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

    public override void SetPossessed(bool possess)
    {
        base.SetPossessed(possess);
        //WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.Smiley);
    }

    public override void PrintPossess()
    {
        int index = UnityEngine.Random.Range(0, textsPossess.Length);
        WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.Smiley, index);
        WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.Possess);
        Console2.Instance.AddFeedback(-1, textsPossess[index], "white");
    }
}
