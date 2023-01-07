using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay
{

    public class EngineerPanel : MonoBehaviour
    {


        public void AssignEngineer(Engineer engineer, ShipComponent shipComponent)
        {
            if (engineer.currentAssigned != null)
            {
                engineer.currentAssigned.RemoveEngineerModifier(engineer.SkillDict[engineer.currentAssigned.Type]);
            }

            engineer.currentAssigned = shipComponent;
            shipComponent.AddEngineerModifier(engineer.SkillDict[shipComponent.Type]);
        }
    }
}