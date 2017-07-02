using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelTextTyper : MonoBehaviour
{
    public class LineToAdd
    {
        public string text;
        public string format;
    }

    public bool inverted;
    public GameObject lipMask;
    public GameObject eyesMask;
    public float timeForChar = 0.001f;

    private UnityEngine.UI.Text textField;
    public UnityEngine.UI.InputField textFieldInput;
    private bool isCorutineRunning = false;

    public List<LineToAdd> lines = new List<LineToAdd>();

    int currentIndex = 0;
    string baseText;
    bool speak = true;
    public event Action OnComplete = () => { };
    bool restart = false;

    public void Start()
    {
        textField = GetComponent<UnityEngine.UI.Text>();
        WorldManager.Instance.soundManager.PlayFinishVoiceOver();
        AppendText("I realized I will never find my one, “true” form.\nBut you helped me along the way.It was a real FailTale™.\nThanks for nothing.", "{0}");
    }

    public void Update()
    {
        if(Input.anyKey && !restart)
        {
            restart = true;
            Restart();
        }
    }

    public void AppendText(string text, string format)
    {
        var line = new LineToAdd();
        line.text = text;
        line.format = format;

        lines.Add(line);

        if (!isCorutineRunning)
            StartCoroutine("TypeText");
        StartCoroutine("Speak");
    }

    public void Skip()
    {
        if (lines.Count <= 0)
            return;
        speak = false;
        StopCoroutine("TypeText");

        var currentLine = lines[lines.Count - 1];

        if (textFieldInput != null)
        {
            if (!inverted)
                textFieldInput.text = baseText + string.Format(currentLine.format, currentLine.text);
            else
                textFieldInput.text = string.Format(currentLine.format, currentLine.text) + baseText;
        }
        else
        {
            if (!inverted)
                textField.text = baseText + string.Format(currentLine.format, currentLine.text);
            else
                textField.text = string.Format(currentLine.format, currentLine.text) + baseText;
        }

        lines.Remove(currentLine);

        if (lines.Count > 0)
        {
            StartCoroutine("TypeText");
            return;
        }

        isCorutineRunning = false;
        OnComplete();
    }

    public IEnumerator TypeText()
    {
        var currentLine = lines[lines.Count - 1];
        baseText = textField.text;

        if (textFieldInput != null)
            baseText = textFieldInput.text;

        currentIndex = 0;
        speak = true;
        isCorutineRunning = true;
        while (currentIndex < currentLine.text.Length)
        {
            if (textFieldInput != null)
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
            yield return new WaitForSeconds(timeForChar);
        }

        lines.Remove(currentLine);
        speak = false;

        if (lines.Count > 0)
        {
            StartCoroutine("TypeText");
            yield break;
        }

        isCorutineRunning = false;
        OnComplete();

        //yield return new WaitForSeconds(3);
      //  WorldManager.Instance.LoadMenu();
    }

    IEnumerator Speak()
    {
        while (speak)
        {
            lipMask.SetActive(!lipMask.activeSelf);
            yield return new WaitForSeconds(0.15f);
        }
        
        eyesMask.SetActive(true);
        lipMask.SetActive(false);
    }

   void Restart()
    {
        if (!speak)
        {
            Application.Quit();
           // WorldManager.Instance.LoadMenu();
        }
    }
}
