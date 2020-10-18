using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Quiz
{
    public class MultiChoice : Base
    {
        [SerializeField] private TextMeshProUGUI answerTextHolder;
        [SerializeField] private GameObject choicePanel;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private int choicesToGenerate;

        private List<string> choicesList;

        public override void OnStart()
        {
            base.OnStart();
            choicesToGenerate = 2 + Mathf.FloorToInt(difficultyModifier);
            choicesList = PopulateChoices(quizSetRef, choicesToGenerate, answer);
            choicesList.Add(answer);
            choicesList.Shuffle();
            GenerateChoiceButtons(choicesList, choicePanel, choiceButtonPrefab, false);

            answerTextHolder.text = question;
        }

        private void GenerateChoiceButtons(List<string> choices, GameObject choicePanel, GameObject buttonPrefab, bool isCorrect)
        {
            foreach (string choice in choices)
            {
                ChoiceButtonScript cbs = Instantiate(buttonPrefab, choicePanel.transform, false).GetComponent<ChoiceButtonScript>();
                cbs.Initialize(isCorrect, this, choice);
            }
        }


        private List<string> PopulateChoices(Dictionary<string, string> quizSet, int numOfChoices, string toAvoid)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < numOfChoices; i++)
            {
                list.Add(LookUpAnswer(quizSet, toAvoid));
            }

            return list;
        }

        //TO DO: Find a better num generator since this one is Shit
        private string LookUpAnswer(Dictionary<string, string> quizSet, string toAvoid)
        {
            string temp = quizSet.ElementAt(Random.Range(0, quizSet.Count)).Value;
            if (temp == toAvoid)
            {
                return LookUpAnswer(quizSet, toAvoid);
            }
            else
            {
                return temp;
            }
        }
    }

    

}