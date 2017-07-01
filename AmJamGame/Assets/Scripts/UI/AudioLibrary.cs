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

    [Header("Effects AudioClips")]
    public List<AudioClip> KeyboardTypesEffectsClips;
    public List<AudioClip> KeyboardEnterEffectsClips;
    public List<AudioClip> RunProgramEffectsClips;
    public List<AudioClip> StepHumanEffectsClips;
    public List<AudioClip> StepFrogEffectsClips;
    public List<AudioClip> StepBalonEffectsClips;
    public List<AudioClip> StepKeyEffectsClips;
    public List<AudioClip> TeleportEffectsClips;
    public List<AudioClip> DeathEffectsClips;
    public List<AudioClip> BlockEffectsClips;
    public List<AudioClip> ErrorEffectsClips;
    public List<AudioClip> FailEffectsClips;
    public List<AudioClip> SuccessEffectsClips;
    public List<AudioClip> PossessEffectsClips;

    [Header("Effects Volume")]
    [Range(50.0f,200.0f)]
    public float KeyboardTypesEffectsClipsVolume = 150.0f;
    [Range(50.0f, 200.0f)]
    public float KeyboardEnterEffectsClipsVolume = 150.0f;
    [Range(50.0f, 200.0f)]
    public float RunProgramEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float StepHumanEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float StepFrogEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float StepBalonEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float StepKeyEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float TeleportEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float DeathEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float BlockEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float ErrorEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float FailEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float SuccessEffectsClipsVolume = 100.0f;
    [Range(50.0f, 200.0f)]
    public float PossessEffectsClipsVolume = 100.0f;

    public AudioClip[] ambientClips;
    //public List<AudioClip> soundEffectsClips;
    
    public AudioClip GetAudioClip(soundEffects origin)
    {
        List<AudioClip> list = GetAudioList(origin);
        int randRange = list.Count;
        if (randRange > 1)
        {
            return list[UnityEngine.Random.Range(0, randRange)];
        }
        else
        {
            return list[0];
        }
    }

    public AudioClip GetAmbientClip(int id = 0)
    {
        return ambientClips[id];
    }

    private List<AudioClip> GetAudioList(soundEffects origin)
    {
        switch (origin)
        {
            case (soundEffects.Block):
                return BlockEffectsClips;
            case (soundEffects.Death):
                return DeathEffectsClips;
            case (soundEffects.Error):
                return ErrorEffectsClips;
            case (soundEffects.Fail):
                return FailEffectsClips;
            case (soundEffects.KeyboardEnter):
                return KeyboardEnterEffectsClips;
            case (soundEffects.KeyboardTypes):
                return KeyboardTypesEffectsClips;
            case (soundEffects.Possess):
                return PossessEffectsClips;
            case (soundEffects.RunProgram):
                return RunProgramEffectsClips;
            case (soundEffects.StepBalon):
                return StepBalonEffectsClips;
            case (soundEffects.StepFrog):
                return StepFrogEffectsClips;
            case (soundEffects.StepHuman):
                return StepHumanEffectsClips;
            case (soundEffects.StepKey):
                return StepKeyEffectsClips;
            case (soundEffects.Success):
                return SuccessEffectsClips;
            case (soundEffects.Teleport):
                return TeleportEffectsClips;
            default:
                return KeyboardTypesEffectsClips;
        }
    }

         private float GetAudioVolume(soundEffects origin)
    {
        switch (origin)
        {
            case (soundEffects.Block):
                return BlockEffectsClipsVolume;
            case (soundEffects.Death):
                return DeathEffectsClipsVolume;
            case (soundEffects.Error):
                return ErrorEffectsClipsVolume;
            case (soundEffects.Fail):
                return FailEffectsClipsVolume;
            case (soundEffects.KeyboardEnter):
                return KeyboardEnterEffectsClipsVolume;
            case (soundEffects.KeyboardTypes):
                return KeyboardTypesEffectsClipsVolume;
            case (soundEffects.Possess):
                return PossessEffectsClipsVolume;
            case (soundEffects.RunProgram):
                return RunProgramEffectsClipsVolume;
            case (soundEffects.StepBalon):
                return StepBalonEffectsClipsVolume;
            case (soundEffects.StepFrog):
                return StepFrogEffectsClipsVolume;
            case (soundEffects.StepHuman):
                return StepHumanEffectsClipsVolume;
            case (soundEffects.StepKey):
                return StepKeyEffectsClipsVolume;
            case (soundEffects.Success):
                return SuccessEffectsClipsVolume;
            case (soundEffects.Teleport):
                return TeleportEffectsClipsVolume;
            default:
                return KeyboardTypesEffectsClipsVolume;
        }

    }


}

