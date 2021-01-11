using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceSystem
{
    public class PlayerMaterialScrollView : MonoBehaviour
    {
        public PlayerProfile playerProfile;
        public GameObject contentHolder;
        public GameObject materialViewPrefab;

        private void Start()
        {
            GenerateResourceView();
        }

        public void GenerateResourceView()
        {
            contentHolder.transform.Clear();

            foreach (Material material in playerProfile.materials)
            {
                MaterialView mv = Instantiate(materialViewPrefab, contentHolder.transform, false).GetComponent<MaterialView>();
                mv.Initialize(material);
            }
        }
    }
}

