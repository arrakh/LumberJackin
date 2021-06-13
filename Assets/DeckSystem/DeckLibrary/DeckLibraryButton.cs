using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DeckLibraryButton : MonoBehaviour
{
    [SerializeField] private Button deckViewButton;
    [SerializeField] private Toggle toggle;
    [SerializeField] private TMP_Text deckNameText;
    [SerializeField] private TMP_Text questionFieldText;
    [SerializeField] private TMP_Text answerFieldText;

    private PlayerProfile profile;
    private List<string> fields;
    private int deckIndex;
    private int questionIndex = 0;
    private int answerIndex = 1;
    private ToggleGroup toggleGroup;

    public void Initialize(int deckIndex, string deckName, List<string> fields, PlayerProfile profile, ToggleGroup toggleGroup, UnityAction onClick)
    {
        this.deckIndex = deckIndex;
        this.fields = fields;
        this.profile = profile;

        toggle.group = toggleGroup;

        deckViewButton.onClick.AddListener(onClick);

        deckNameText.text = deckName;

        if(profile.activeDeckIndex == deckIndex)
        {
            questionIndex = GetSetting().questionIndex;
            answerIndex = GetSetting().answerIndex;
        }

        CycleAnswer(0);
        CycleQuestion(0);
    }

    public void OnToggled(bool isToggled)
    {
        if (isToggled) profile.activeDeckIndex = deckIndex;
    }

    public void CycleQuestion(int offset)
    {
        do 
        { 
            questionIndex += offset;
            if (questionIndex > fields.Count - 1) questionIndex = 0;
            else if (questionIndex < 0) questionIndex = fields.Count - 1;
        } while (answerIndex == questionIndex);

        questionFieldText.text = fields[questionIndex];
        GetSetting().questionIndex = questionIndex;
    }

    public void CycleAnswer(int offset)
    {
        do
        {
            answerIndex += offset;
            if (answerIndex > fields.Count - 1) answerIndex = 0;
            else if (answerIndex < 0) answerIndex = fields.Count - 1;
        } while (answerIndex == questionIndex);

        answerFieldText.text = fields[answerIndex];
        GetSetting().answerIndex = answerIndex;
    }

    private Quiz.QuizSetting GetSetting() => profile.decks[deckIndex].quizSetting;
}
