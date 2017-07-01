using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : Singleton<TextManager> {

    #region  syntax errors

    string[] BracketsLack =
        {
       "Forgetting brackets, are we?",
        "Mind the brackets, genius.",   
"Error, the player doesn’t know how to add brackets."
        };

    string[] CommandError =
    {
       "Error, command not recognized. Big surprise.",
"I don’t understand, what command is this?",
"Wrong command, try again, pls."
        };

    string[] WrongArgumentsNumber =
{
"Wrong number of arguments, look up the reference.",
"Wrong argument count, focus!",
"You’re not a programmer, are you? Wrong number of arguments."
        };

    string[] WrongArgumentsFirst =
{
"Fix your first argument, bro.",
"First argument error. Try to fix it.",
"Something went wrong there, fix your first argument."
        };

    string[] WrongArgumentsSecond =
{
"The second argument is wrong, you mind taking a look at it?",
"Something wrong with the second argument.",
"Second argument error: wrong. It’s just wrong."
        };

    string[] SemicolonLack =
{
"You know that thing right to the “r” button? The semicolon, yes. Use it next time.",
"End the line with a semicolon, try to remember that.",
"Methods should end with semicolon, k?" 
        };

    #endregion

    #region methods

    public string GetBacketsText()
    {
        return BracketsLack[Random.Range(0, BracketsLack.Length)];
    }
    public string GetWrongCommandText()
    {
        return CommandError[Random.Range(0, CommandError.Length)];
    }
    public string GetWrongNumerText()
    {
        return WrongArgumentsNumber[Random.Range(0, WrongArgumentsNumber.Length)];
    }
    public string GetWrongFirstText()
    {
        return WrongArgumentsFirst[Random.Range(0, WrongArgumentsFirst.Length)];
    }
    public string GetWrongSecondText()
    {
        return WrongArgumentsSecond[Random.Range(0, WrongArgumentsSecond.Length)];
    }
    public string GetSemicolonText()
    {
        return SemicolonLack[Random.Range(0, SemicolonLack.Length)];
    }
    #endregion

}
