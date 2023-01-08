using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Gameplay
{

    public class EngineerPanel : SerializedMonoBehaviour
    {
        [SerializeField] private AssignmentSwitch[] switches;
        [SerializeField] private EngineerSelection currentEngineerSelection;
        public Engineer currentEngineer;

        private void Awake()
        {
            
        }

        private void Start()
        {
            currentEngineerSelection.OnSelect();
            UpdateSwitchboard();
        }

        public void SelectEngineer( Engineer engineer, EngineerSelection selection)
        {
            currentEngineerSelection.OnDeselect();
            currentEngineer = engineer;
            currentEngineerSelection = selection;
            currentEngineerSelection.OnSelect();
            UpdateSwitchboard();
        }

        public void AssignEngineer(ShipComponent shipComponent)
        {
            if (currentEngineer.currentAssigned != null)
            {
                currentEngineer.currentAssigned.RemoveEngineerModifier(currentEngineer.SkillDict[currentEngineer.currentAssigned.Type]);
            }
            
            currentEngineer.currentAssigned = shipComponent;
            shipComponent.AddEngineerModifier(currentEngineer.SkillDict[shipComponent.Type]);
            UpdateSwitchboard();
        }

        public void UpdateSwitchboard()
        {
            foreach (var sw in switches)
            {
                if (currentEngineer.currentAssigned == null)
                {
                    sw.Deselect();
                    continue;
                }
                if (sw.ShipComponent != currentEngineer.currentAssigned)  sw.Deselect();
                else sw.Select();
            }
        }
    }
}