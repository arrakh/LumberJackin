using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine.SceneManagement;

public class TaskPicker : MonoBehaviour
{
    [SerializeField] private TaskSetting taskSetting;
    [SerializeField] private SimpleScrollSnap scrollSnap;

    public void DoTask()
    {
        taskSetting.activeIndex = scrollSnap.CurrentPanel;
        SceneManager.LoadScene("S_Task");
    }
}
