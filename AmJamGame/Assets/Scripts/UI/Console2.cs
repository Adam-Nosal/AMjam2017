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

    private List<LineWithFeedback> CurrentRunLines = new List<LineWithFeedback>();
    private int previousLinesCount = 0;

    bool sthChangedInInput = true;

    // Use this for initialization
    void Start () {
       
       // ConsoleInput.OnSubmit += InputEntered;

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

       //// Debug.Log(ConsoleInput.selectionFocusPosition);
       // int currentLine = 0;
       // int charsInLines = 0; 
       //for(int i=0;i< CurrentRunLines.Count; i++)
       // {
       //     charsInLines += (CurrentRunLines[i].code!=null?CurrentRunLines[i].code.Length:0) + (CurrentRunLines[i].feedback != null ? CurrentRunLines[i].feedback.Length : 0);
       //     if (ConsoleInput.selectionFocusPosition<=charsInLines)
       //     {
       //         currentLine = i;
       //       //  Debug.Log("CURRENT LINE " + currentLine); 
       //         break;
       //     }
       // }

        // if (CurrentRunLines.Count > currentLine && !string.IsNullOrEmpty(CurrentRunLines[currentLine].feedback))
        // {
        //     string[] lines = ConsoleInput.text.Split('\n');
        //     // for (int i = 0; i < lines.Length; i++)
        //     //  {
        //     if (CurrentRunLines.Count > currentLine && !string.IsNullOrEmpty(CurrentRunLines[currentLine].feedback))
        //     {
        //         lines[currentLine] = lines[currentLine].Remove(lines[currentLine].IndexOf(CurrentRunLines[currentLine].feedback), CurrentRunLines[currentLine].feedback.Length);
        //         CurrentRunLines[currentLine].feedback = "";

        //         ConsoleInput.text = "";
        //         for (int i = 0; i < CurrentRunLines.Count; i++)
        //             ConsoleInput.text += (i == 0 ? "" : "\n") + CurrentRunLines[i].code + CurrentRunLines[i].feedback;
        //     }
        //     //  }
        //     // lines[i] = lines[i].Remove(lines[ConsoleInput.selectionFocusPosition].IndexOf(CurrentRunLines[i].feedback), CurrentRunLines[i].feedback.Length);
        //     Debug.Log(ConsoleInput.selectionFocusPosition);
        // }
    }

    public void InputEntered()
    {
      //  EventSystem.current.SetSelectedGameObject(ConsoleInput.gameObject, null);
     //   ConsoleInput.OnPointerClick(new PointerEventData(EventSystem.current));


        //Debug.Log(ConsoleInput.text);
        //CurrentRunLines.Add(ConsoleInput.text);
        //ConsoleOutputText.text += "\n" + ConsoleInput.text;
        //CommandInterpreter.Instance.usedCommandsList.Add(ConsoleInput.text);
       // ConsoleInput.text = "";
    }

    public void RunButtonClicked()
    {
        //  CurrentRunLines.Clear();
        // string[] lines = ConsoleInput.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //  ClearFeedback();

        string[] lines = ConsoleInput.text.Split('\n');

        //for (int i = 0; i < lines.Length; i++)
        //{
        //    if(CurrentRunLines.Count>i && !string.IsNullOrEmpty(CurrentRunLines[i].feedback))
        //        lines[i] = lines[i].Remove(lines[i].IndexOf(CurrentRunLines[i].feedback), CurrentRunLines[i].feedback.Length);
        //}

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

        // ConsoleOutputText.text += "\n<color=yellow>RUN</color>";
        // CurrentRunLines.Add("\n<color=yellow>RUN</color>");
       // previousLinesCount += CurrentRunLines.Count - previousLinesCount;

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
}
