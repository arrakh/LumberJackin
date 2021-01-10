using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceSystem
{
    public class RewardScrollView : MonoBehaviour
    {
        [SerializeField] private GameObject contentHolder;
        [SerializeField] private GameObject materialViewPrefab;

        public void Initialize(List<KeyValuePair<Material, int>> materialToShow)
        {
            contentHolder.transform.Clear();

            foreach (KeyValuePair<Material, int> material in materialToShow)
            {
                MaterialView mv = Instantiate(materialViewPrefab, contentHolder.transform, false).GetComponent<MaterialView>();
                mv.materialIcon.sprite = material.Key.ResourceIcon;
                mv.materialText.text = material.Value.ToString();
            }
        }
    }
}

