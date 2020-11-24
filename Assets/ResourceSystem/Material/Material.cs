using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceSystem
{
    [CreateAssetMenu(fileName ="Material", menuName = "Resource System/Material Item")]
    public class Material : BaseResource
    {
        [SerializeField] private int amount;

        public override int GetResourceValue()
        {
            return amount;
        }
    }

}
