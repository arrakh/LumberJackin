﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class AnkiParser
{
    public static Deck GetDeckFromJson(string json)
    {
        Deck deck = new Deck();

        JSONNode parsed = JSON.Parse(json);

        //Get Field Name
        JSONArray arr = parsed["note_models"][0]["flds"].AsArray;

        for (int i = 0; i < arr.Count; i++)
        {
            deck.fieldNames.Add(arr[i]["name"].Value);
        }

        bool hasGeneratedFieldTypes = false;

        //Store Note fields
        arr = parsed["notes"].AsArray;

        for (int i = 0; i < arr.Count; i++)
        {
            //Create new temp Note
            Note newNote = new Note();

            //Assign GUID
            newNote.UID = arr[i]["guid"].Value;

            //Assign fields
            JSONArray fieldArr = arr[i]["fields"].AsArray;

            for (int j = 0; j < fieldArr.Count; j++)
            {
                //cache field value
                string note = fieldArr[j].Value;

                //cache type and detect + format field value
                FieldType type = deck.DetectFieldType(note, out note);

                if (!hasGeneratedFieldTypes)
                {
                    //Store field type. this will only run on the first set of note
                    deck.fieldTypes.Add(type);
                }

                //add field to note
                newNote.Fields.Add(note);
            }

            hasGeneratedFieldTypes = true;

            //Add to deck notes
            deck.notes.Add(newNote);

            //Debug.Log($"Capital: {newNote.Fields[0]}, UID: {newNote.UID}");
        }
                
        return deck;
    }

    //public static List<T> GetListFromArray<T>(JSONArray array)
    //{
    //    List<T> list = new List<T>();
    //    for (int i = 0; i < array.Count; i++)
    //    {
    //        list.Add((T)array[i].);
    //    }
    //}
}
