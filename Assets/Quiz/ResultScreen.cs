using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private Button homeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private ResourceSystem.RewardScrollView rewardScrollView;

    public void Initialize(int point, int totalCard, int totalCorrect, int totalWrong, List<KeyValuePair<ResourceSystem.Material, int>> rewards)
    {
        homeButton.onClick.AddListener(delegate { SceneManager.LoadScene("S_Menu"); });
        retryButton.onClick.AddListener(delegate { SceneManager.LoadScene("S_Task"); });

        resultText.text =
            $"Point: {point}" +
            $"\nTotal Card: {totalCard}" +
            $"\nTotal Correct: {totalCorrect}" +
            $"\nTotal Incorrect: {totalWrong}" +
            $"\n\n\nGet Resource:";

        rewardScrollView.Initialize(rewards);
    }

}
