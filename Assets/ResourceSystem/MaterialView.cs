using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceSystem
{
    public class MaterialView : MonoBehaviour
    {
        [SerializeField] private Image materialIcon;
        [SerializeField] private TextMeshProUGUI materialText;

        public void Initialize(Material materialToView)
        {
            if (materialToView.GetResourceValue() <= 0)
            {
                this.gameObject.SetActive(false); 
                return;
            }
            materialIcon.sprite = materialToView.ResourceIcon;
            materialText.text = materialToView.GetResourceValue().ToString();
        }
    }
}

