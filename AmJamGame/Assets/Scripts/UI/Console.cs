using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Console : Singleton<Console> {

    public InputField ConsoleInput;
    public Text ConsoleOutputText;

    private List<string> CurrentRunLines = new List<string>();
    private int previousLinesCount = 0;

    // Use this for initialization
    void Start () {
       // ConsoleInput.OnSubmit += InputEntered;

    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log(ConsoleInput.selectionFocusPosition);  
    }

    public void InputEntered()
    {
      //  EventSystem.current.SetSelectedGameObject(ConsoleInput.gameObject, null);
     //   ConsoleInput.OnPointerClick(new PointerEventData(EventSystem.current));


        Debug.Log(ConsoleInput.text);
        CurrentRunLines.Add(ConsoleInput.text);
        ConsoleOutputText.text += "\n" + ConsoleInput.text;
        CommandInterpreter.Instance.usedCommandsList.Add(ConsoleInput.text);
        ConsoleInput.text = "";
    }

    public void RunButtonClicked()
    {
        //  CurrentRunLines.Clear();
        CommandInterpreter.Instance.InterpretCommands();
        CommandInterpreter.Instance.usedCommandsList.Clear();
       // ConsoleOutputText.text += "\n<color=yellow>RUN</color>";
       // CurrentRunLines.Add("\n<color=yellow>RUN</color>");
        previousLinesCount += CurrentRunLines.Count - previousLinesCount;

    }

    public void AddFeedback(int line, string feedback)
    {
      //  if(CurrentRunLines==null)
      //      Console2.In

        //string[] lines = ConsoleOutputText.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        CurrentRunLines[previousLinesCount + line] += " <color=red>" + feedback + "</color>";
        ConsoleOutputText.text = "";
        foreach (var ln in CurrentRunLines)
            ConsoleOutputText.text += "\n" + ln;
    }
}
