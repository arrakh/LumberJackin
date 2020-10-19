using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{
    public class QuizManager : MonoBehaviour
    {
        public Dictionary<string, string> quizSet = new Dictionary<string, string>();
        public Action OnComplete;
        public float difficultyModifier = 1f;

        private Queue<string> quizQueue;
        private GameObject currentQuizObj;

        [SerializeField] private List<GameObject> quizType;
        [SerializeField] private GameObject quizPanel;

        //Temporary debug before a proper deck importer / scheduler
        [SerializeField] private bool useDebugSet;
        [SerializeField] DebugSet debugSetToUse;

        #region Singleton Instance
        private static QuizManager _instance;

        public static QuizManager Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        #endregion

        private void Start()
        {
            //TO DO: Write a proper json importer for anki decks, put it in quizset and queue the desired cards based on review
            //Temporary debug before a proper deck importer / scheduler
            if (useDebugSet)
            {
                //Add list to quizSet Dictionary
                foreach (DebugQuiz quiz in debugSetToUse.contents)
                {
                    quizSet.Add(quiz.question, quiz.answer);
                }
            }

            //TO DO: queue cards based on desired amount of cards to review + new cards
            List<string> temp = new List<string>(quizSet.Keys);
            quizQueue = temp.Shuffle().ToQueue();

            //Spawn new quiz type based on available quiz types
            SpawnNewQuiz();
        }

        public void SpawnNewQuiz()
        {
            SpawnNewQuiz(UnityEngine.Random.Range(0, quizType.Count - 1));
        }

        public void SpawnNewQuiz(int quizIndex)
        {
            SpawnNewQuiz(quizType[quizIndex]);
        }

        public void SpawnNewQuiz(GameObject quizObject)
        {
            Destroy(currentQuizObj);
            currentQuizObj = Instantiate(quizObject, quizPanel.transform, false);
            Base qb = currentQuizObj.GetComponent<Base>();
            
            //Quiz Base Variable Init
            qb.OnAnswered += OnQuizAnswered;
            qb.difficultyModifier = difficultyModifier;
            qb.quizSetRef = quizSet;

            //take queue out and store both question and answer
            qb.question = quizQueue.Dequeue();
            qb.answer = quizSet[qb.question];

            //Start Quiz
            qb.OnStart();
        }

        public void OnQuizAnswered(bool isCorrect, float points)
        {
            Debug.Log(isCorrect + " / " + points);
            SpawnNewQuiz();
        }

    }
}


