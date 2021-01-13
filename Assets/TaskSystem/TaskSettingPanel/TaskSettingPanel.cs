using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSettingPanel : MonoBehaviour
{
    [SerializeField] private PlayerProfile playerProfile;
    [SerializeField] private TaskSetting taskSetting;
    [SerializeField] private GameObject contentPrefab;
    [Space(20f)]
    [SerializeField] private SimpleScrollSnap currentDeckSSS;
    [SerializeField] private SimpleScrollSnap questionSSS;
    [SerializeField] private SimpleScrollSnap answerSSS;

    private void OnEnable()
    {
        playerProfile.activeDeckIndex = 0;
        UpdateCurrentDeck();
        UpdateQuestion();
        UpdateAnswer();
    }

    public void UpdateCurrentDeck()
    {
        currentDeckSSS.RemoveAll();

        foreach (Deck deck in playerProfile.decks)
        {
            TextWithBG tbg = currentDeckSSS.AddToBack(contentPrefab).GetComponent<TextWithBG>();
            tbg.Initialize(deck.deckName);
        }

        currentDeckSSS.GoToPanel(playerProfile.activeDeckIndex);
    }

    public void SetCurrentDeck()
    {

        playerProfile.activeDeckIndex = currentDeckSSS.CurrentPanel;

        UpdateQuestion();
        UpdateAnswer();
    }
    
    public void UpdateQuestion()
    {
        questionSSS.RemoveAll();

        foreach (string field in playerProfile.GetActiveDeck().notes[0].Fields)
        {
            TextWithBG tbg = questionSSS.AddToBack(contentPrefab).GetComponent<TextWithBG>();
            tbg.Initialize(field);
        }

        questionSSS.GoToPanel(playerProfile.GetActiveDeck().quizSetting.questionIndex);
    }

    //Yes this is stupid i know
    public void UpdateAnswer()
    {
        answerSSS.RemoveAll();

        foreach (string field in playerProfile.GetActiveDeck().notes[0].Fields)
        {
            TextWithBG tbg = answerSSS.AddToBack(contentPrefab).GetComponent<TextWithBG>();
            tbg.Initialize(field);
        }

        answerSSS.GoToPanel(playerProfile.GetActiveDeck().quizSetting.answerIndex);
        Debug.Log(answerSSS.TargetPanel);
    }

    public void SetQuestionIndex() => playerProfile.GetActiveDeck().quizSetting.questionIndex = questionSSS.CurrentPanel;
    public void SetAnswerIndex() => playerProfile.GetActiveDeck().quizSetting.answerIndex = answerSSS.CurrentPanel;


}

