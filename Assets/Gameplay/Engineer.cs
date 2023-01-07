using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "New Engineer", menuName = "SASG/Engineer", order = 0)]
    public class Engineer : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<ComponentTypes, int> SkillDict;
        public ShipComponent currentAssigned = null;

        private void Awake()
        {
            SkillDict = new Dictionary<ComponentTypes, int>
            {
                { ComponentTypes.Blaster, 1 },
                { ComponentTypes.Ftl, 1 },
                { ComponentTypes.Helm, 1 },
                { ComponentTypes.FCore, 1 },
                { ComponentTypes.Shield, 1 },
                { ComponentTypes.Torpedo, 1 },
                { ComponentTypes.LifeSupport, 1 }
            };
        }
    }
}