using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ResourceSystem
{
    public class MaterialView : MonoBehaviour
    {
        [SerializeField] public Image materialIcon;
        [SerializeField] public TextMeshProUGUI materialText;
        [SerializeField] private Material toView;
        [SerializeField] private bool useToView;
        [SerializeField] private bool disableOnZero = true;
        [SerializeField] private bool staticAmount;

        private void Start()
        {
            if (useToView) Initialize(toView);
        }

        public void Initialize(Material materialToView)
        {
            if (materialToView.amount <= 0 && disableOnZero)
            {
                this.gameObject.SetActive(false); 
                return;
            }

            materialIcon.sprite = materialToView.ResourceIcon;

            if (!staticAmount)
            {
                materialToView.OnAmountChanged += UpdateAmount;
                UpdateAmount(materialToView.amount);
            }
        }

        private void UpdateAmount(int amount)
        {
            materialText.text = amount.ToString();
        }
    }
}

