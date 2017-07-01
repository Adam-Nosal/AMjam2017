using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenuAttribute(fileName = "AudioLibrary")]
public class AudioLibrary : ScriptableObject
{
    public enum soundEffects
    {
        KeyboardTypes=0,
        KeyboardEnter=1,
        RunProgram=2,
        StepHuman=3,
        StepFrog=4,
        StepBalon=5,
        StepKey=6,
        Teleport=7,
        Death=8,
        Block=9,
        Error=10,
        Fail=11,
        Success=12,
        Possess=13

    }

    public AudioClip[] ambientClips;
    public List<AudioClip> soundEffectsClips;

    public AudioClip GetAudioClip(soundEffects origin)
    {
       return soundEffectsClips[(int)origin];
    }

    public AudioClip GetAmbientClip(int id = 0)
    {
        return ambientClips[id];
    }


}

