using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Note
{
    private int delay;

    public List<string> Fields = new List<string>();

    public string UID { get; set; }

    public int Delay
    {
        get
        {
            return delay;
        }
        set
        {
            if (value < 0)
            {
                delay = 0;
            }
            else
            {
                delay = value;
            }
        }
    }
}
