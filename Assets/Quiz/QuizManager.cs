using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{
    public class QuizManager : MonoBehaviour
    {
        //TO DO: Refactor this whole sh*t based on the new Deck and Note objects, sigh.
        public Deck currentDeck;


        public Dictionary<string, string> quizSet = new Dictionary<string, string>();
        public Action OnComplete;
        public float difficultyModifier = 1f;
        public float currentPoints = 0f;

        private Queue<string> quizQueue;
        private string lastQuiz;
        private GameObject currentQuizObj;

        [SerializeField] private List<GameObject> quizType;
        [SerializeField] private GameObject quizPanel;
        [SerializeField] private NoteView.NoteView noteView;
        [SerializeField] private float delayBtwQuiz = 2.0f;

        //Temporary debug before a proper deck importer / scheduler
        [SerializeField] private bool useDebugSet;
        [SerializeField] DebugSet debugSetToUse;
        [SerializeField] TextAsset json;

        //Temp for anim testing
        [SerializeField] Animator tempDude;

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

            //Temporary debug before a proper deck importer / scheduler
            if (useDebugSet)
            {
                //Add list to quizSet Dictionary
                foreach (DebugQuiz quiz in debugSetToUse.contents)
                {   
                    quizSet.Add(quiz.question, quiz.answer);
                }
            }

            //TO DO: Lazy sync on a deck collection instead
            currentDeck = AnkiParser.GetDeckFromJson(json.text);

            foreach (Note note in currentDeck.notes)
            {
                if (!quizSet.ContainsKey(note.Fields[0]))
                {
                    quizSet.Add(note.Fields[0], note.Fields[4]);
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
            //Destroy current quiz
            Destroy(currentQuizObj);

            //Check if quiz queue is empty
            if(quizQueue.Count != 0)
            {
                currentQuizObj = Instantiate(quizObject, quizPanel.transform, false);
                Base qb = currentQuizObj.GetComponent<Base>();

                //Quiz Base Variable Init
                qb.OnAnswered += OnQuizAnswered;
                qb.difficultyModifier = difficultyModifier;
                qb.quizSetRef = quizSet;

                //QUESTION: Why dequeue it now? just dequeue OnQuizAnswered. Will probably be taken care of when we eventually switch to Deck&Note system though
                //take queue out and store both question and answer
                qb.question = quizQueue.Dequeue();
                qb.answer = quizSet[qb.question];

                //Store last quiz to be evaluated after quiz is answered
                lastQuiz = qb.question;

                //Start Quiz
                qb.OnStart();
            }
            else
            {
                OnCompleteQuiz();
            }
        }

        public void OnQuizAnswered(bool isCorrect, float points)
        {
            Debug.Log(isCorrect + " / " + points);

            //Add points to current points
            currentPoints += points;

            //Stuff to do when answer is correct
            if (isCorrect)
            {
                //Spawn quiz after a delay, will spawn randomly by default
                Invoke("SpawnNewQuiz", delayBtwQuiz);
            }
            //Stuff to do when answer is wrong
            else
            {
                Invoke("ShowNoteView", delayBtwQuiz / 2);
                //Add wrong answer to end of queue
                quizQueue.Enqueue(lastQuiz);
            }

            

            //TO DO: Send this to a "game scene" that will be spawned on start later

            //TEMP, DELETE LATER
            string animToTrigger = isCorrect ? "Attack" : "Hurt";
            tempDude.SetTrigger(animToTrigger);

            OnCompleteQuiz();
        }

        public void ShowNoteView()
        {
            noteView.gameObject.SetActive(true);
            noteView.Initialize(currentDeck, OhGodPleaseDeleteThisStupidCodeLater(lastQuiz));

        }

        public void OnCompleteQuiz()
        {
            OnComplete?.Invoke();
        }

        //PLEASE REFACTOR PLEASE OH GOD
        private Note OhGodPleaseDeleteThisStupidCodeLater(string fieldToCheck)
        {
            foreach (Note note in currentDeck.notes)
            {
                if (note.Fields.Contains(fieldToCheck)) return note;
            }
            return null;
        }

        [Button]
        public void Test()
        {
            AnkiParser.GetDeckFromJson(json.text);
        }

    }
}


