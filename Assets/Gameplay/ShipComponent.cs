using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace Gameplay
{
    public enum ComponentTypes
    {
        None = 0,
        Shield = 1,
        LifeSupport = 2,
        Ftl = 3,
        Blaster = 4,
        Helm = 5,
        Torpedo = 6,
        FCore = 7,
    }
    
    public class ShipComponent : MonoBehaviour
    {
        public bool isBroken = false;
        private int PowerInput = 1;
        [SerializeField] private ComponentTypes type = ComponentTypes.None;
        [SerializeField] private float breakageChance = .4f;
        [SerializeField] private ShipManager Ship;
        private float EngineerModifier;
        [SerializeField] private float repairTime = 15f;
        private float currentRepairTime;

        public ComponentTypes Type => type;

        private void Awake()
        {
            
        }

        private void Start()
        {
            Ship.Tick += Tick;
        }

        public void SetPowerInput(int newInput)
        {
            if (newInput is <= 0 or >= 3) return;
            PowerInput = newInput;
        }

        private void Tick()
        {
            if (PowerInput == 1) return;
            if (!isBroken && breakageChance / EngineerModifier <= Random.value)
            {
                isBroken = true;
                repairTime = 0;
            }
            
            switch (type)
            {
                case ComponentTypes.LifeSupport:
                    Ship.TickLifeSupport(isBroken);
                    break;
                case ComponentTypes.Shield:
                    Ship.TickShield(isBroken);
                    break;
            }

            if (!isBroken || EngineerModifier == 0) return;
            
            currentRepairTime += Time.deltaTime * EngineerModifier;
            if (currentRepairTime >= repairTime)
            {
                isBroken = false;
            }
        }

        public void AddEngineerModifier(float modifier)
        {
            EngineerModifier += modifier;
        }

        public void RemoveEngineerModifier(float modifier)
        {
            EngineerModifier -= modifier;
        }
    }
}