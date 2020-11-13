using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{

    public class Base : MonoBehaviour
    {
        public Action<bool, float> OnAnswered;
        public float points = 1f;
        public string question;
        public string answer;

        //TODO: Create a scriptable object for global variable instead
        public float difficultyModifier = 1f;
        public Dictionary<string, string> quizSetRef = new Dictionary<string, string>();

        public virtual void OnStart() { }
        public virtual void OnAnswer(bool isCorrect)
        {
            OnAnswered.Invoke(isCorrect, OnCalculatePoints(isCorrect));
        }

        public virtual float OnCalculatePoints(bool isCorrect)
        {
            return points;
        }
    }

}
