using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrueFalseCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text correctText;
    [SerializeField] private TMP_Text wrongText;
    [SerializeField] private Quiz.QuizManager quizManager;

    private void OnEnable()
    {
        quizManager.OnQuizResultUpdate += OnUpdate;
    }

    private void OnDisable()
    {
        quizManager.OnQuizResultUpdate -= OnUpdate;
    }

    private void OnUpdate(float score, int correct, int wrong)
    {
        correctText.text = correct.ToString();
        wrongText.text = wrong.ToString();
    }
}
