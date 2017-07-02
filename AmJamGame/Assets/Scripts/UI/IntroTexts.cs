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

    private Coroutine printCor;
    bool isWaiting = false;
    bool isWaitingRemove = false;

    // Use this for initialization
    IEnumerator Start ()
    {
        inputField.interactable = false;
        instruction.enabled = false;
        ButtonRun.SetActive(false);

        typer.OnComplete += Typer_OnComplete;

        yield return new WaitForSeconds(1f);
        printCor = StartCoroutine(PrintLine(0f));
    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.anyKeyDown)
        {
            if(isWaiting)
            {
                isWaiting = false;
                StopCoroutine("PrintLine");
                typer.AppendText(lines[currentLine], "{0}");
            }
            else if(isWaitingRemove)
            {
                StopCoroutine("RemoveIntro");
                isWaitingRemove = false;
                RemoveIntroFromScreen();
            }
            else
            {
                typer.Skip();
            }          
           
        }
	}

    private IEnumerator PrintLine(float time)
    {
        isWaiting = true;
        yield return new WaitForSeconds(time);
        if (lines.Length > currentLine)
            typer.AppendText(lines[currentLine], "{0}");
        isWaiting = false;
    }

    private void Typer_OnComplete()
    {        
        currentLine++;
        inputField.text += "\n\n";

        if (lines.Length > currentLine)
        {
            printCor = StartCoroutine(PrintLine(timeBetweenlines));
        }
        else
        {
            typer.OnComplete -= Typer_OnComplete;
            StartCoroutine("RemoveIntro");
        }
    }
    private IEnumerator RemoveIntro()
    {
        isWaitingRemove = true;
        yield return new WaitForSeconds(8f);
        RemoveIntroFromScreen();
    }

    private void RemoveIntroFromScreen()
    {
        inputField.text = "";
        inputField.interactable = true;
        instruction.enabled = true;
        ButtonRun.SetActive(true);
        inputField.Select();
    }
}
