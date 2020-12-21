using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public string taskID;
    public GameObject gameViewPrefab;
    public bool isUnlocked;
    public ResourceSystem.Tool usingTool;
    public List<TaskReward> rewards;
}

[System.Serializable]
public class TaskReward
{
    public ResourceSystem.Material materialReward;
    public int minRandomRange;
    public int maxRandomRange;
}
