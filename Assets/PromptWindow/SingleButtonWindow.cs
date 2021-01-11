using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PromptWindow
{
    public class SingleButtonWindow : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] GameObject contentHolder;

        public GameObject Initialize(GameObject content, string buttonText, UnityAction onClick)
        {
            button.onClick.AddListener(onClick);
            button.onClick.AddListener(delegate { CloseWindow(); });
            button.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
            GameObject obj = Instantiate(content, contentHolder.transform, false);
            return obj;
        }

        public void CloseWindow()
        {

            Destroy(this.gameObject);
        }
    }
}

