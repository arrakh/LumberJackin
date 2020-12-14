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
    }
    
    public class RequirementResourceView : MonoBehaviour
    {
        public List<MaterialRequirement> requireMaterials;
        public GameObject contentHolder;
        public GameObject materialViewPrefab;

        private void Start()
        {
            GenerateResourceView();
        }

        public void GenerateResourceView()
        {
            contentHolder.transform.Clear();

            foreach (MaterialRequirement material in requireMaterials)
            {
                MaterialView mv = Instantiate(materialViewPrefab, contentHolder.transform, false).GetComponent<MaterialView>();
                mv.materialText.text = material.reqAmount.ToString();
                mv.materialIcon.sprite= material._material.ResourceIcon;
                //mv.Initialize(material);
            }
        }
    }
}

