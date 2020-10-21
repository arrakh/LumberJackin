using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

namespace Quiz
{
    public class ChoiceButtonScript : MonoBehaviour
    {
        private Button btn;
        private Base baseQuiz;

        [SerializeField] private TextMeshProUGUI btnText;
        [SerializeField] private Image btnImg;
        [SerializeField] private Color correctColor;
        [SerializeField] private Color incorrectColor;

        public bool isCorrect;
        public int index;

        private void Start()
        {
            btn = GetComponent<Button>();
        }

        public void Initialize(bool isCorrectButton, Base baseQuiz, string text, int index)
        {
            //Set needed variables
            btn = GetComponent<Button>();
            this.baseQuiz = baseQuiz;
            isCorrect = isCorrectButton;
            btnText.text = text;
            this.index = index;

            //Add OnAnswer delegate on button click
            btn.onClick.AddListener(delegate { baseQuiz.OnAnswer(isCorrectButton); });

            //Add tween spawn animation
            StartCoroutine(SpawnAnim());
        }

        private IEnumerator SpawnAnim()
        {
            btn.transform.DOScale(0f, 0f);
            yield return new WaitForSeconds(index * 0.025f);
            btn.transform.DOScale(1f, 0.1f);
        }

        //This func will be called on all buttons after done answering
        public void OnDoneAnswering()
        {
            //Set Color
            btnImg.color = isCorrect ? correctColor : incorrectColor;

            //Set can't interact
            btn.interactable = false;

            if (isCorrect)
            {

            }
            else
            {

            }
        }
    }
}
