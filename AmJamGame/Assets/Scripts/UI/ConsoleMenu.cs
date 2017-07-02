using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConsoleMenu : Singleton<ConsoleMenu> {

   public class LineWithFeedback
    {
       public string code;
       public string feedback;
    }

    public InputField ConsoleInput;
    public Text DebugOutput;

    private bool areCredits = false;
    private List<LineWithFeedback> CurrentRunLines = new List<LineWithFeedback>();
    private int previousLinesCount = 0;

    // Use this for initialization
    void Start () {
       
       // ConsoleInput.OnSubmit += InputEntered;

    }
	
	// Update is called once per frame
	void Update () {

       if(ConsoleInput.isFocused)
        {
            int index = CurrentRunLines.FindIndex(l => !string.IsNullOrEmpty(l.feedback));
            if (index >= 0)
            {
                ConsoleInput.text = "";
                CurrentRunLines[index].feedback = "";
                for (int i = 0; i < CurrentRunLines.Count; i++)
                    ConsoleInput.text += (i == 0 ? "" : "\n") + CurrentRunLines[i].code;
            }
        }

    
    }

    public void InputEntered()
    {
  
    }

    public void RunButtonClicked()
    {
      
        string[] lines = ConsoleInput.text.Split('\n');


        CurrentRunLines.Clear();

        for (int i = 0; i < CurrentRunLines.Count; i++)
            CurrentRunLines[i].feedback = "";

        for (int i = 0; i < CurrentRunLines.Count; i++)
            ConsoleInput.text += (i == 0 ? "" : "\n") + CurrentRunLines[i].code;



        foreach (var ln in lines)
        {
            CurrentRunLines.Add( new LineWithFeedback() { code = ln });
            MenuCommandInterpreter.Instance.usedCommandsList.Add(ln);
        }

        MenuCommandInterpreter.Instance.InterpretCommands();
        MenuCommandInterpreter.Instance.usedCommandsList.Clear();
        

    }

    public void AddFeedback(int line, string feedback)
    {
        if (areCredits)
            StartCoroutine(ClearOutput());
        CurrentRunLines[line].feedback = feedback;// " <color=red>" + feedback + "</color>";
         ConsoleInput.text = "";
        for (int i = 0; i < CurrentRunLines.Count; i++)
            if (i == line)
                ConsoleInput.text += (i == 0 ? "" : "\n") + "<color=red>" + CurrentRunLines[i].code + "</color>";
            else
                ConsoleInput.text += (i == 0 ? "" : "\n") + CurrentRunLines[i].code;

        DebugOutput.text = "<color=red>" + feedback + "</color>\n" + DebugOutput.text;
        
    }

    public IEnumerator ClearOutput()
    {
        yield return new WaitForEndOfFrame();
        areCredits = false;
        DebugOutput.text = string.Empty;

        yield return new WaitForEndOfFrame();
    }

    public void PrintCredits(string credits)
    {
        areCredits = true;
        ClearOutput();
        DebugOutput.text = credits;
    }

    void ClearFeedback()
    {
        for (int i = 0; i < CurrentRunLines.Count; i++)
            CurrentRunLines[i].feedback = "";

        for (int i = 0; i < CurrentRunLines.Count; i++)
            ConsoleInput.text += (i == 0 ? "" : "\n") + CurrentRunLines[i].code;;

    }
}
