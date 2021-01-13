using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextWithBG : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text text;

    public void Initialize(string value, Sprite bg = null)
    {
        text.text = value;
        if (bg) background.sprite = bg;
    }
}

