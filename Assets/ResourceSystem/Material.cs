using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceSystem
{
    [CreateAssetMenu(fileName ="Material", menuName = "Resource System/Material Item")]
    public class Material : BaseResource
    {
        [SerializeField] private int _amount;
        public int amount 
        {
            get => _amount;
            set
            {
                if (value <= 0) _amount = 0;
                else _amount = value;
                OnAmountChanged?.Invoke(_amount);
            } 
        }
        public Action<int> OnAmountChanged;

        public override int GetResourceValue()
        {
            return amount;
        }
    }

}
