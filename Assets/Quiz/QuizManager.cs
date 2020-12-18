using NaughtyAttributes;
using PromptWindow;
using NoteView;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Quiz
{
    public class QuizManager : MonoBehaviour
    {
        public Deck currentDeck;

        //Refactor to use Note and Deck instead
        public Dictionary<string, string> quizSet = new Dictionary<string, string>();
        public Action OnComplete;
        public float currentPoints = 0f;

        //private Queue<string> quizQueue;
        //private string lastQuiz;

        private Queue<Note> noteQueue;
        private List<Note> noteSet = new List<Note>();

        private GameObject currentQuizObj;
        private QuizSetting quizSetting;
        private GameView currentGameView;

        [SerializeField] private PlayerProfile playerProfile;
        [SerializeField] private List<GameObject> quizType;
        [SerializeField] private GameObject gameView;
        [SerializeField] private GameObject quizPanel;
        [SerializeField] private GameObject promptHolder;
        [SerializeField] private GameObject promptPrefab;
        [SerializeField] private GameObject noteViewPrefab;
        [SerializeField] private float delayBtwQuiz = 2.0f;

        [SerializeField] private GameObject promptHolder_TEMP;
        [SerializeField] private GameObject materialScrollView_TEMP;
        [SerializeField] private GameObject blackAlpha_TEMP;

        //Temporary debug before a proper deck importer / scheduler
        [SerializeField] private bool useDebugSet;
        [SerializeField] DebugSet debugSetToUse;
        [SerializeField] TextAsset json;

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

            currentGameView = Instantiate(gameView).GetComponent<GameView>();

            //Load current Deck based on Player Profile
            currentDeck = playerProfile.decks[playerProfile.activeDeckIndex];
            currentDeck.notes.Shuffle();
            quizSetting = currentDeck.quizSetting;

            for (int i = 0; i < quizSetting.maxCardPerTask; i++)
            {
                Note noteToAdd;
                do
                {
                    noteToAdd = currentDeck.notes[UnityEngine.Random.Range(0, currentDeck.notes.Count - 1)];
                } while (noteSet.Contains(noteToAdd));
                noteSet.Add(noteToAdd);
            }

            noteQueue = noteSet.ToQueue();

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

            //Dequeue quiz
            noteQueue.Dequeue();

            //Check if quiz queue is empty
            if(noteQueue.Count > 0)
            {
                currentQuizObj = Instantiate(quizObject, quizPanel.transform, false);
                Base qb = currentQuizObj.GetComponent<Base>();

                //Quiz Base Variable Init
                qb.OnAnswered += OnQuizAnswered;
                qb.noteSetRef = noteSet;
                //qb.quizSetRef = quizSet;

                //Peek queue and set Base's question & answer
                Note note = noteQueue.Peek();
                qb.question = note.Fields[currentDeck.quizSetting.questionIndex];
                qb.answer = note.Fields[currentDeck.quizSetting.answerIndex];

                qb.setting = quizSetting;

                //Start Quiz
                qb.OnStart();
            }
            else
            {
                Instantiate(promptPrefab, promptHolder_TEMP.transform, false)
                    .GetComponent<SingleButtonWindow>()
                    .Initialize(materialScrollView_TEMP, "Yay!", OnCompleteQuiz);
                blackAlpha_TEMP.SetActive(true);
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
                noteQueue.Enqueue(noteQueue.Peek());
            }

            //Trigger Answer func on GameView
            currentGameView.OnAnswer(isCorrect);
        }

        public void ShowNoteView()
        {
            SingleButtonWindow sbw = Instantiate(promptPrefab, promptHolder.transform, false).GetComponent<SingleButtonWindow>();
            GameObject noteGO = sbw.Initialize(noteViewPrefab, "OK", delegate { SpawnNewQuiz(); });
            NoteViewScript nv = noteGO.GetComponent<NoteViewScript>();
            nv.Initialize(currentDeck, noteQueue.Peek());
        }

        public void OnCompleteQuiz()
        {
            OnComplete?.Invoke();
            SceneManager.LoadScene("S_Menu");
        }

    }
}


