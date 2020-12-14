using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceSystem
{
    [CreateAssetMenu(fileName = "Tool", menuName = "Resource System/Tool Item")]
    public class Tool : BaseResource
    {
        [SerializeField] public int level;
        public Action<int> OnLevelChange;
        public List<ToolUpgrade> upgrades;
        public override int GetResourceValue()
        {
            return level;
        }
        
        
        public void Upgrade(ToolUpgrade toolUpgrade)
        {
            foreach (MaterialRequirement materialRequirement in toolUpgrade.requirements)
            {
                materialRequirement._material.amount -= materialRequirement.reqAmount;
            }

            level = toolUpgrade.level;
            OnLevelChange?.Invoke(level);
        }
        
        public ToolUpgrade GetToolUpgrade(int level)
        {
            foreach (ToolUpgrade toolUpgrade in upgrades)
            {
                if (level == toolUpgrade.level)
                {
                    return toolUpgrade;
                }
            }

            return null;
        }
    }

    [System.Serializable]
    public class ToolUpgrade
    {
        public int level;
        public List<MaterialRequirement> requirements;

      
        public bool CanUpgrade()
        {
            foreach (MaterialRequirement materialRequirement in requirements)
            {
                //Debug.Log(materialRequirement.IsRequirementFulfilled());
                bool value = materialRequirement.IsRequirementFulfilled();
                if (value == false)
                {
                    Debug.Log(value);
                    return false;
                }
            }
            return true;
        }
    }
}


