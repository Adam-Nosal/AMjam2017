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
        AudioClip clip = audioLibrary.GetAudioClip(origin);
        if (clip != null)
        {
            effectsAudioSource.clip = clip;

            effectsAudioSource.Play();
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
