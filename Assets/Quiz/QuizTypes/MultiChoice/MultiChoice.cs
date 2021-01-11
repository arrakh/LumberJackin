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
        [SerializeField] private BonusBar bonusBar;
        [SerializeField] private int choicesToGenerate;

        private List<string> choicesList = new List<string>();
        private List<ChoiceButtonScript> btns = new List<ChoiceButtonScript>();

        //==============================================- OVERRIDE FUNCTIONS -======================================================
        public override void OnStart()
        {
            base.OnStart();

            //Calculate how many options to show. default with diff of 1 will be 4 options, 2 will be 5 options, and so on
            choicesToGenerate = 3 + Mathf.FloorToInt(difficultyModifier);

            //Add answer to choices List
            choicesList.Add(answer);

            //Fill list with random choices based on note set ref
            choicesList = FillList(noteSetRef, choicesToGenerate, choicesList);

            //Shuffle list
            choicesList.Shuffle();

            //Generate buttons based on choicesList
            GenerateChoiceButtons(choicesList, choicePanel, choiceButtonPrefab);

            answerTextHolder.text = question;
        }

        public override float OnCalculatePoints(bool isCorrect)
        {
            points = isCorrect ? bonusBar.value : 0.0f;
            return points;
        }

        public override void OnAnswer(bool isCorrect)
        {
            //call done answer in each button
            foreach (ChoiceButtonScript cbs in btns)
                cbs.OnDoneAnswering();

            //Stop Bonus bar
            bonusBar.stopBar = true;

            //call base override func
            base.OnAnswer(isCorrect);
        }
        //==========================================================================================================================

        private void GenerateChoiceButtons(List<string> choices, GameObject choicePanel, GameObject buttonPrefab)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                //Spawn and initialize button based on answer
                ChoiceButtonScript cbs = Instantiate(buttonPrefab, choicePanel.transform, false).GetComponent<ChoiceButtonScript>();
                bool isCorrect = choices[i] == answer ? true : false;
                cbs.Initialize(isCorrect, this, choices[i], i);

                //Add to button array
                btns.Add(cbs);
            }
        }

        private List<string> FillList(List<Note> noteSetRef, int max, List<string> listToFill)
        {
            //Is list already filled
            if (listToFill.Count < max)
            {
                //Get new entry on set
                string newEntry = noteSetRef[Random.Range(0, noteSetRef.Count)].Fields[setting.answerIndex];

                //Does the new entry not exist on the list?
                if (!listToFill.Contains(newEntry))
                {
                    listToFill.Add(newEntry);
                }

                return FillList(noteSetRef, max, listToFill);
            }
            else
            {
                return listToFill;
            }
        }
          
    }

    

}