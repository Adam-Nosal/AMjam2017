using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour {

    public InputField ConsoleInput;

	// Use this for initialization
	void Start () {
       // ConsoleInput.OnSubmit += InputEntered;

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("enter");
        }       
    }

    public void InputEntered()
    {
        Debug.Log(ConsoleInput.text);
        CommandInterpreter.Instance.usedCommandsList.Add(ConsoleInput.text);
       // bool isRecognisible = CommandInterpreter.Instance.InterpretCommand(ConsoleInput.text);

     ///   if()
    }

}
