using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    [SerializeField]
    AudioSource ambientAudioSource;

    [SerializeField]
    AudioSource effectsAudioSource;


    [SerializeField]
    AudioSource voiceOverAudioSource;


    AudioLibrary audioLibrary;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);
        audioLibrary = WorldManager.Instance.GetAudioLibrary();
        if (ambientAudioSource == null)
        {
            ambientAudioSource = this.gameObject.AddComponent<AudioSource>();
            ambientAudioSource.clip = audioLibrary.GetAmbientClip();
            ambientAudioSource.volume = audioLibrary.AmbientClipsVolume;
            ambientAudioSource.loop = true;
        }
        if (effectsAudioSource == null)
        {
            effectsAudioSource = this.gameObject.AddComponent<AudioSource>();
            effectsAudioSource.clip = audioLibrary.GetAmbientClip();
            effectsAudioSource.loop = false;
        }
        if (voiceOverAudioSource == null)
        {
            voiceOverAudioSource = this.gameObject.AddComponent<AudioSource>();
            voiceOverAudioSource.clip = audioLibrary.GetVoiceOverClip(0);
            voiceOverAudioSource.loop = false;
        }
    }

    [ContextMenu("PlayAmbient")]
    public void PlayAmbient()
    {
        ambientAudioSource.Play();
    }

    public void PlayEffect( AudioLibrary.soundEffects origin)
    {
        AudioClip clip = audioLibrary.GetAudioClip(origin);
        if (clip != null)
        {
            effectsAudioSource.clip = clip;
            effectsAudioSource.volume = audioLibrary.GetAudioVolume(origin);
            effectsAudioSource.Play();
        }
    }

    public void PlayVoiceOverByType(AudioLibrary.VoiceOverEffects origin)
    {
        AudioClip clip = audioLibrary.GetVoiceOverClip(origin);
        if (clip != null)
        {
            effectsAudioSource.clip = clip;
            effectsAudioSource.volume = audioLibrary.VoiceOversVolume;
            effectsAudioSource.Play();
        }
    }

    public void PlayVoiceOver(int index)
    {
        AudioClip clip = audioLibrary.GetVoiceOverClip(index);
        if (clip != null)
        {
            voiceOverAudioSource.clip = clip;
            voiceOverAudioSource.volume = audioLibrary.VoiceOversVolume;
            voiceOverAudioSource.Play();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.KeyboardEnter);
        }else if(Input.anyKeyDown== true)
        {
            WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.KeyboardTypes);
        }
    }

}
