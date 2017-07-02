using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTexts : MonoBehaviour
{
    public string[] lines;
    public float timeBetweenlines;

    public UnityEngine.UI.Text instruction;
    public UnityEngine.UI.InputField inputField;
    public GameObject ButtonRun;
    public TextTyper typer;

    private int currentLine;

	// Use this for initialization
	IEnumerator Start ()
    {
        inputField.interactable = false;
        instruction.enabled = false;
        ButtonRun.SetActive(false);

        typer.OnComplete += Typer_OnComplete;

        yield return new WaitForSeconds(1f);
        StartCoroutine(PrintLine(0f));
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StopCoroutine("PrintLine");
            currentLine = 500;
            typer.Skip();
            StartCoroutine(RemoveIntro());
        }
	}

    private IEnumerator PrintLine(float time)
    {
        yield return new WaitForSeconds(time);
        typer.AppendText(lines[currentLine], "{0}");
    }

    private void Typer_OnComplete()
    {        
        currentLine++;
        inputField.text += "\n\n";

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
        ButtonRun.SetActive(true);
        inputField.Select();
    }

}
