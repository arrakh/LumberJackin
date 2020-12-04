using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{
    [System.Serializable]
    public class QuizSetting
    {
        public float difficultyModifier = 1f;
        public int questionIndex;
        public int answerIndex;
        public int maxCardPerTask = 10;
    }
}