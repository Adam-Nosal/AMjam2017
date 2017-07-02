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
"You know that thing right to the “l” button? The semicolon, yes. Use it next time.",
"End the line with a semicolon, try to remember that.",
"Methods should end with semicolon, k?" 
        };

    #endregion

    #region methods


    // WARNING! HARD CODED OFFETS HERE 


    public string GetBacketsText()
    {
        int index = Random.Range(0, BracketsLack.Length);
        WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.SyntaxError, index);
        return BracketsLack[index];
    }
    public string GetWrongCommandText()
    {
        int index = Random.Range(0, CommandError.Length);
        WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.SyntaxError, index +3);
        return CommandError[index];
    }
    public string GetWrongNumerText()
    {
        int index = Random.Range(0, WrongArgumentsNumber.Length);
        WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.SyntaxError, index + 6);
        return WrongArgumentsNumber[index];
    }
    public string GetWrongFirstText()
    {
        int index = Random.Range(0, WrongArgumentsFirst.Length);
        WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.SyntaxError, index + 9);
        return WrongArgumentsFirst[index];
    }
    public string GetWrongSecondText()
    {
        int index = Random.Range(0, WrongArgumentsSecond.Length);
        WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.SyntaxError, index + 12);

        return WrongArgumentsSecond[index];
    }
    public string GetSemicolonText()
    {
        int index = Random.Range(0, SemicolonLack.Length);
        WorldManager.Instance.soundManager.PlayVoiceOverByType(AudioLibrary.VoiceOverEffects.SyntaxError, index + 15);


        return SemicolonLack[index];
    }
    #endregion

}
