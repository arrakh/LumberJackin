using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class ChoiceButtonScript : MonoBehaviour
    {
        private Button btn;
        private Base baseQuiz;
        [SerializeField] private TextMeshProUGUI btnText;
        public bool isCorrect;

        private void Start()
        {
            btn = GetComponent<Button>();
        }

        public void Initialize(bool isCorrectButton, Base baseQuiz, string text)
        {
            btn = GetComponent<Button>();
            this.baseQuiz = baseQuiz;
            isCorrect = isCorrectButton;
            btnText.text = text;
            btn.onClick.AddListener(delegate { baseQuiz.OnAnswer(isCorrectButton); });
        }
    }
}
