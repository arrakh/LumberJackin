using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusBar : MonoBehaviour
{
    public float duration = 1f;
    public float value;
    public bool stopBar = false;
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderFill;
    [SerializeField] private Color fullColor;
    [SerializeField] private Color emptyColor;
    [SerializeField] private TextMeshProUGUI bonusText;

    private void Update()
    {
        if(slider.value > 0 && !stopBar)
        {
            //Set slider value
            slider.value = slider.value - (Time.deltaTime / duration);

            //TO DO: Separate data and display
            value = (float)System.Math.Round(((slider.value * 3f) + 1f), 1);

            //Interpolate colors
            sliderFill.color = Color.Lerp(emptyColor, fullColor, slider.value);

            //Set bonus text
            bonusText.text = value <= 0 ? "x1.0" : ("Bonus x" + value);
        }
    }
}
