using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    [SerializeField]
    AudioSource ambientAudioSource;
    [SerializeField]
    float ambientVolume = 100.0f;
    [SerializeField]
    AudioSource effectsAudioSource;
    [SerializeField]
    float effectsVolume = 100.0f;

    AudioLibrary audioLibrary;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);
        audioLibrary = WorldManager.Instance.GetAudioLibrary();
        if (ambientAudioSource == null)
        {
            ambientAudioSource = this.gameObject.AddComponent<AudioSource>();
            ambientAudioSource.clip = audioLibrary.GetAmbientClip();
            ambientAudioSource.volume = ambientVolume;
            ambientAudioSource.loop = true;
        }
        if (effectsAudioSource == null)
        {
            effectsAudioSource = this.gameObject.AddComponent<AudioSource>();
            effectsAudioSource.clip = audioLibrary.GetAmbientClip();
            effectsAudioSource.loop = false;
            effectsAudioSource.volume = effectsVolume;
        }
    }

    [ContextMenu("PlayAmbient")]
    public void PlayAmbient()
    {

        ambientAudioSource.Play();
    }

    public void PlayEffect( AudioLibrary.soundEffects origin)
    {
        effectsAudioSource.clip = audioLibrary.GetAudioClip(origin);
   
        effectsAudioSource.Play();
    }
    
    
}
