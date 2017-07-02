using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Console2 : Singleton<Console2> {

   public class LineWithFeedback
    {
       public string code;
       public string feedback;
    }

    public InputField ConsoleInput;
    public Text DebugOutput;
    public Text Scores;

    private List<LineWithFeedback> CurrentRunLines = new List<LineWithFeedback>();
    private int previousLinesCount = 0;

    bool sthChangedInInput = true;

    // Use this for initialization
    void Start () {
       
        //ConsoleInput.OnSubmit += InputEntered;

    }
	
	// Update is called once per frame
	void Update () {

       if(ConsoleInput.isFocused)
        {
            sthChangedInInput = true;
            int index = CurrentRunLines.FindIndex(l => !string.IsNullOrEmpty(l.feedback));
            if (index >= 0)
            {
                ConsoleInput.text = "";
                CurrentRunLines[index].feedback = "";
                for (int i = 0; i < CurrentRunLines.Count; i++)
                {
                    //CurrentRunLines[index].feedback = "";

                    ConsoleInput.text += (i == 0 ? "" : "\n") + CurrentRunLines[i].code;
                }
           }
        }
       
            if (Input.GetKeyDown(KeyCode.F5))
            {
            RunButtonClicked();
            }
        

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
        {
            UpdateScores();
        }
    }

    public void InputEntered()
    {
    }

    public void RunButtonClicked()
    {
        GameManager.Instance.executionsCount++;        
       
        WorldManager.Instance.soundManager.PlayEffect(AudioLibrary.soundEffects.RunProgram);
        string[] lines = ConsoleInput.text.Split('\n');
    

        if (sthChangedInInput)
        {
            CurrentRunLines.Clear();

            for (int i = 0; i < CurrentRunLines.Count; i++)
                CurrentRunLines[i].feedback = "";

            for (int i = 0; i < CurrentRunLines.Count; i++)
                ConsoleInput.text += (i == 0 ? "" : "\n") + CurrentRunLines[i].code;
        }
        
        for (int i=0;i< lines.Length;i++)
        {
            if (sthChangedInInput)
                CurrentRunLines.Add(new LineWithFeedback() { code = lines[i] });
            CommandInterpreter.Instance.usedCommandsList.Add(CurrentRunLines[i].code);
        }
        sthChangedInInput = false;

        CommandInterpreter.Instance.InterpretCommands();
        CommandInterpreter.Instance.usedCommandsList.Clear();

        UpdateScores();
    }

    public void AddFeedback(int line, string feedback, string color="red")
    {
        var colorKey = string.Format("<color={0}>", color);

        if (line >= 0)
        {
            //string[] lines = ConsoleOutputText.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            // CurrentRunLines[line].feedback = "";
            CurrentRunLines[line].feedback = feedback;// " <color=red>" + feedback + "</color>";
                                                      // CurrentRunLines[line].code = " <color=red>" + feedback + "</color>";
            ConsoleInput.text = "";
            for (int i = 0; i < CurrentRunLines.Count; i++)
                if (i == line)
                    ConsoleInput.text += (i == 0 ? "" : "\n") + colorKey + CurrentRunLines[i].code + "</color>";
                else
                    ConsoleInput.text += (i == 0 ? "" : "\n") + CurrentRunLines[i].code;
        }

        if (!string.IsNullOrEmpty(DebugOutput.text))
                DebugOutput.text = "\n" + DebugOutput.text;
        

        DebugOutput.GetComponent<TextTyper>().AppendText(feedback, colorKey + "{0}</color>");
    }

    void UpdateScores()
    {
        Scores.text = string.Format("Revision: {0}\n\nLines: {1}", GameManager.Instance.executionsCount, ConsoleInput.text.Split('\n').Length);

    }
}
