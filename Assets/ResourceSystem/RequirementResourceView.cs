using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace ResourceSystem
{

    [System.Serializable]
    public class MaterialRequirement
    {
        public Material _material;
        public int reqAmount;


        public bool IsRequirementFulfilled()
        {
            return _material.amount >= reqAmount;
        }
    }
    
    public class RequirementResourceView : MonoBehaviour
    {
        public Tool toolToView;
        public GameObject contentHolder;
        public TextMeshProUGUI currentLevelText;
        public GameObject materialViewPrefab;

        private void Start()
        {
            UpdateLevelText(toolToView.level);
            toolToView.OnLevelChange += UpdateLevelText;
        }
        
        public void UpdateLevelText(int level)
        {
            currentLevelText.text = level.ToString();
            GenerateResourceView();
        }

        public void GenerateResourceView()
        {
            contentHolder.transform.Clear();

            foreach (MaterialRequirement material in toolToView.GetToolUpgrade(toolToView.level+1).requirements)
            {
                MaterialView mv = Instantiate(materialViewPrefab, contentHolder.transform, false).GetComponent<MaterialView>();
                mv.materialText.text = material.reqAmount.ToString();
                mv.materialIcon.sprite= material._material.ResourceIcon;
                //mv.Initialize(material);
            }
        }
    }
}

