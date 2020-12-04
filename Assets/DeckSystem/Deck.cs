using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum FieldType
{
    Text,
    Cloze,
    Image,
    Sound
}

[Serializable]
public class Deck
{
    //Deck Vars
    public string deckName;
    public string deckMediaPath;
    public Quiz.QuizSetting quizSetting;
    public List<string> fieldNames      = new List<string>();
    public List<Note> notes             = new List<Note>();
    public List<FieldType> fieldTypes   = new List<FieldType>();

    //Deck Settings
    private string imagePrefix      = "<img src=\"";
    private string imagePostfix     = "\" />";
    private string soundPrefix      = "[sound:";
    private string soundPostfix     = "]";
    private string clozeFormat      = "";

    public List<Note> GetDueNotes()
    {
        List<Note> dueNotes = notes.Where(x => x.Delay == 0).ToList();
        return dueNotes;
    }

    public FieldType DetectFieldType (string note, out string formattedNote)
    {
        if (note.StartsWith(imagePrefix))
        {
            formattedNote = note.Replace(imagePrefix, string.Empty).Replace(imagePostfix, string.Empty);
            return FieldType.Image;
        }
        else if (note.StartsWith(soundPrefix))
        {
            formattedNote = note.Replace(soundPrefix, string.Empty).Replace(soundPostfix, string.Empty);
            return FieldType.Sound;
        } 
        //else if (note.Contains(clozeFormat))
        //{
        //    formattedNote = note.Replace(clozeFormat, string.Empty);
        //    return FieldType.Cloze;
        //}
        else
        {
            formattedNote = note;
            return FieldType.Text;
        }
    }
}
