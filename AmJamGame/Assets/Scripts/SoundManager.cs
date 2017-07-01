using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    [SerializeField]
    AudioSource ambientAudioSource;
    [SerializeField]
    float ambientVolume = 100.0f;
    //[SerializeField]
    //AudioSource effectsAudioSource;

    AudioLibrary audioLibrary;

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);
        audioLibrary = WorldManager.Instance.GetAudioLibrary();
        ambientAudioSource = this.gameObject.AddComponent<AudioSource>();
        ambientAudioSource.clip = audioLibrary.GetAmbientClip();
        ambientAudioSource.volume = ambientVolume;
        ambientAudioSource.loop = true;
    }

    [ContextMenu("PlayAmbient")]
    public void PlayAmbient()
    {

        ambientAudioSource.Play();
    }
    
    
}
