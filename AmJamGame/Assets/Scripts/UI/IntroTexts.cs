using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTexts : MonoBehaviour
{
    public string[] lines;
    public float timeBetweenlines;

    public UnityEngine.UI.Text instruction;
    public UnityEngine.UI.InputField inputField;
    public TextTyper typer;

    private int currentLine;

	// Use this for initialization
	IEnumerator Start ()
    {
        inputField.interactable = false;
        instruction.enabled = false;

        yield return new WaitForSeconds(1f);
        StartCoroutine(PrintLine(0f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator PrintLine(float time)
    {
        yield return new WaitForSeconds(time);
        typer.AppendText(lines[currentLine], "{0}");
        typer.OnComplete += Typer_OnComplete;
    }

    private void Typer_OnComplete()
    {        
        currentLine++;
        inputField.text += "\n";

        if (lines.Length > currentLine)
        {
            StartCoroutine(PrintLine(timeBetweenlines));
        }
        else
        {
            StartCoroutine(RemoveIntro());
        }
    }
    private IEnumerator RemoveIntro()
    {
        yield return new WaitForSeconds(8f);
        inputField.text = "";
        inputField.interactable = true;
        instruction.enabled = true;
        inputField.Select();
    }

}
