using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck
{
    public List<string> fieldNames = new List<string>();
    public List<Note> notes = new List<Note>();

    public List<Note> GetDueNotes()
    {
        List<Note> dueNotes = notes.Where(x => x.Delay == 0).ToList();
        return dueNotes;
    }
}
