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

        //private Queue<string> quizQueue;
        //private string lastQuiz;

        private Queue<Note> noteQueue;
        private List<Note> noteSet = new List<Note>();

        private GameObject currentQuizObj;
        private QuizSetting quizSetting;
        private GameView currentGameView;
        private List<TaskReward> currentRewards;

        [SerializeField] private PlayerProfile playerProfile;
        [SerializeField] private TaskSetting taskSetting;
        [SerializeField] private List<GameObject> quizType;
        [SerializeField] private GameObject quizPanel;
        [SerializeField] private GameObject resultPanel;
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

        //Audio Stuff
        [Header("Audio")]
        [SerializeField] private AudioClip correctSFX;
        [SerializeField] private AudioClip wrongSFX;
        [SerializeField] private AudioSource sfxSource;
        
        //Score stuff
        private float currentPoints = 0f;
        private int totalCorrect;
        private int totalWrong;

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

            currentGameView = Instantiate(taskSetting.GetActiveTask().gameViewPrefab).GetComponent<GameView>();
            currentRewards = taskSetting.GetActiveTask().rewards;

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
                qb.question = note.Fields[quizSetting.questionIndex];
                qb.answer = note.Fields[quizSetting.answerIndex];

                qb.setting = quizSetting;

                //Start Quiz
                qb.OnStart();
            }
            else
            {
                OnCompleteQuiz();
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
                totalCorrect++;

                sfxSource.PlayOneShot(correctSFX);

                //Spawn quiz after a delay, will spawn randomly by default
                Invoke("SpawnNewQuiz", delayBtwQuiz);
            }
            //Stuff to do when answer is wrong
            else
            {
                totalWrong++;

                sfxSource.PlayOneShot(wrongSFX);

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

            //Seriously what the fuck is this code even
            List<KeyValuePair<ResourceSystem.Material, int>> mats = new List<KeyValuePair<ResourceSystem.Material, int>>();

            var multiplier = taskSetting.GetActiveTask().usingTool.level;
            foreach (TaskReward reward in currentRewards)
            {
                int rewardAmount = UnityEngine.Random.Range(reward.minRandomRange, reward.maxRandomRange) * multiplier;
                reward.materialReward.amount += rewardAmount;

                //No, seriously. What the actual fuck. Why.
                mats.Add(new KeyValuePair<ResourceSystem.Material, int>(reward.materialReward, rewardAmount));
            }

            resultPanel.SetActive(true);
            ResultScreen rs = resultPanel.GetComponent<ResultScreen>();
            rs.Initialize(Mathf.FloorToInt(currentPoints), quizSetting.maxCardPerTask, totalCorrect, totalWrong, mats);

        }

    }
}


