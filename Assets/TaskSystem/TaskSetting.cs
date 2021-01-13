using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Task Setting")]
public class TaskSetting : ScriptableObject
{
    public List<Task> tasks;
    public int activeIndex;
    public Quiz.QuizSetting quizSetting;

    public Task GetActiveTask() => tasks[activeIndex];
}
