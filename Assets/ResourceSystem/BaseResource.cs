using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceSystem
{
    public class BaseResource : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string displayName;
        [SerializeField] private string description;
        [SerializeField] private Sprite resourceIcon;

        public string Id
        {
            get => id;
            set => id = value;
        }
        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }
        public string Description
        {
            get => description;
            set => description = value;
        }
        public Sprite ResourceIcon
        {
            get => resourceIcon;
            set => resourceIcon = value;
        }

        virtual public int GetResourceValue() { return 0; }
    }
}


