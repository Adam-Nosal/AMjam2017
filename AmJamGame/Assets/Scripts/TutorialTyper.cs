using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTyper : MonoBehaviour
{
    public string[] lines;
    public TextTyper textTyper;
    public UnityEngine.UI.InputField inputField;

    public AudioLibrary.VoiceOverEffects audioLib;

    public GameObject[] lockGO;

    public string[] predefinedCommands;

    public float timeBetweenLines;

    private int currentLine = -1;
    private bool isInputActive = false;

    public void Start()
    {
        foreach (var go in lockGO)
            go.SetActive(false);

        inputField.interactable = false;
        textTyper.OnComplete += TextTyper_OnComplete;
        StartCoroutine(DelayPlayNextLine());
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
        WorldManager.Instance.soundManager.PlayVoiceOverByType(audioLib, currentLine);
        inputField.text += "\n";
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
            inputField.Select();
            foreach (var command in predefinedCommands)
                inputField.text += command + "\n";

            inputField.interactable = true;

            foreach (var go in lockGO)
                go.SetActive(true);

            isInputActive = false;
        }
    }
}
