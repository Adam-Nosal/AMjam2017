using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTyper : MonoBehaviour
{
    public class LineToAdd
    {
        public string text;
        public string format;
    }

    public bool inverted;

    private UnityEngine.UI.Text textField;
    public UnityEngine.UI.InputField textFieldInput;
    private bool isCorutineRunning = false;

    public List<LineToAdd> lines = new List<LineToAdd>();


    public event Action OnComplete = ()=> { };

    public void Start()
    {
        textField = GetComponent<UnityEngine.UI.Text>();        
    }

    public void AppendText(string text, string format)
    {
        var line = new LineToAdd();
        line.text = text;
        line.format = format;

        lines.Add(line);

        if (!isCorutineRunning)
            StartCoroutine(TypeText());
    }

    public void Skip()
    {
        OnComplete();
    }
	
    public IEnumerator TypeText()
    {
        var currentLine = lines[lines.Count-1];
        var baseText = textField.text;

        if (textFieldInput != null)
            baseText = textFieldInput.text;

        var currentIndex = 0;

        isCorutineRunning = true;
        while (currentIndex < currentLine.text.Length)
        {
            if(textFieldInput != null)
            {
                if (!inverted)
                    textFieldInput.text = baseText + string.Format(currentLine.format, currentLine.text.Substring(0, currentIndex + 1));
                else
                    textFieldInput.text = string.Format(currentLine.format, currentLine.text.Substring(0, currentIndex + 1)) + baseText;
            }
            else
            {
                if (!inverted)
                    textField.text = baseText + string.Format(currentLine.format, currentLine.text.Substring(0, currentIndex + 1));
                else
                    textField.text = string.Format(currentLine.format, currentLine.text.Substring(0, currentIndex + 1)) + baseText;
            }

            currentIndex++;

            yield return new WaitForEndOfFrame();
        }

        lines.Remove(currentLine);

        if (lines.Count > 0)
        {
            StartCoroutine(TypeText());
            yield break;
        }
        

        isCorutineRunning = false;
        OnComplete();
    }
}
