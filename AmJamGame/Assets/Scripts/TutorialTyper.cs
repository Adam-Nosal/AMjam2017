using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTyper : MonoBehaviour
{
    public string[] lines;
    public TextTyper textTyper;
    public UnityEngine.UI.InputField inputField;

    public float timeBetweenLines;

    private int currentLine = -1;
    private bool isInputActive = false;

    public void Start()
    {
        textTyper.OnComplete += TextTyper_OnComplete;
    }

    private void TextTyper_OnComplete()
    {
        if (currentLine < lines.Length - 1)
        {
            StartCoroutine(DelayPlayNextLine());
        }
        else
        {
            ActivateInput();
        }
    }

    private IEnumerator DelayPlayNextLine()
    {
        yield return new WaitForSeconds(timeBetweenLines);
        PlayNextLine();
    }

    private void PlayNextLine()
    {
        currentLine++;
        WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.Intro, currentLine);
        textTyper.AppendText(lines[currentLine], "{0}");
    }
    
    private void ActivateInput()
    {
        isInputActive = true;
        textTyper.OnComplete -= TextTyper_OnComplete;
    }

    private void Update()
    {
        if(isInputActive && Input.anyKeyDown)
        {
            inputField.text = "";
            inputField.interactable = true;
            inputField.Select();

            isInputActive = false;
        }
    }
}
