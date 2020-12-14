
using System.Collections.Generic;
using UnityEngine;


namespace ResourceSystem
{
    public class RequireUpgradeHandler : MonoBehaviour
    {
        private Material _material;
        private Tool _tool;
        [SerializeField] private List<RequirementResourceView> _resourceViews;

        public void Upgrade(Tool tool, int level)
        {
            if (tool.level < level)
            {
                ToolUpgrade toolUpgrade = tool.GetToolUpgrade(level);
                Debug.Log(toolUpgrade.level);
                if (toolUpgrade.CanUpgrade())
                {
                    tool.Upgrade(toolUpgrade);
                }
            }
        }

        public void Upgrade(Tool tool)
        {
            Upgrade(tool, tool.level+1);
        }
        

    }
}

