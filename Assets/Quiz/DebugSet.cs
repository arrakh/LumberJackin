using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Quiz
{
    [Serializable]
    public class DebugQuiz
    {
        public string question;
        public string answer;
    }

    [CreateAssetMenu(fileName = "Debug Quiz")]
    public class DebugSet : ScriptableObject
    {
        public List<DebugQuiz> contents;
        [SerializeField] private int generateNum = 10;

        [Button]
        public void GenerateNumContents()
        {
            contents.Clear();
            for (int i = 0; i < generateNum; i++)
            {
                DebugQuiz dq = new DebugQuiz();
                dq.question = "(" + i + ")";
                dq.answer = i.ToString();
                contents.Add(dq);
            }
        }
    }
}