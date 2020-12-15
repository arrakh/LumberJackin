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
        [SerializeField] private bool useToView = false;
        [SerializeField] private bool disableOnZero = true;

        private void Start()
        {
            if (useToView) Initialize(toView);
        }

        public void Initialize(Material materialToView)
        {
            if (materialToView.GetResourceValue() <= 0 && disableOnZero)
            {
                this.gameObject.SetActive(false); 
                return;
            }
            materialIcon.sprite = materialToView.ResourceIcon;
            materialText.text = materialToView.GetResourceValue().ToString();
        }
    }
}

