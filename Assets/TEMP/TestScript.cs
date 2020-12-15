using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public List<Human> humans;

    // Start is called before the first frame update
    void Start()
    {
        Human arya = GetHumanByName("hoho");
    }

    public Human GetHumanByName(string name)
    {
        foreach (Human human in humans)
        {
            if(human.name == name)
            {
                return human;
            }
        }
        return null;
    }
}

[System.Serializable]
public class Human
{
    public string name;
    public int age;
    public string job;
    public DateData birthday;
}

[System.Serializable]
public class DateData
{
    public int year;
    public int month;
    public int day;
}
