using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceSystem
{
    [CreateAssetMenu(fileName = "Tool", menuName = "Resource System/Tool Item")]
    public class Tool : BaseResource
    {
        [SerializeField] private int level;

        public override int GetResourceValue()
        {
            return level;
        }
    }
}


