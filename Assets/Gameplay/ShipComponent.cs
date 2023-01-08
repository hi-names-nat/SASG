using System;
using Sirenix.OdinInspector;
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
        private float breakageChance = .4f;
        [SerializeField] private float standardBreakageChance = .4f;
        [SerializeField] private float lowPowerBreakageChance = .1f;
        [SerializeField] private float overclockBreakageChance = .6f;
        [SerializeField] private int lowPowerCost = 2;
        [SerializeField] private int MedPowerCost = 4;
        [SerializeField] private int HiPowerCost = 8;
        [SerializeField] private float noPowerBreakChance = .2f;
        [SerializeField] private WarningUI _warningUI;
        private int currentPowerCost;
        

        [SerializeField] private ShipManager Ship;
        [SerializeField, ReadOnly] private float EngineerModifier = 1;
        [SerializeField, ReadOnly] private int numEngineersAssigned = 0;

        [SerializeField] private float repairTime = 15f;
        private float currentRepairTime;

        [SerializeField] private ComponentStatusUI _statusUI;

        public ComponentTypes Type => type;

        private void Awake()
        {
            currentPowerCost = MedPowerCost;
            _statusUI.SetPowerInput(MedPowerCost);
        }

        private void Start()
        {
            Ship.Tick += Tick;
        }

        public void SetPowerInput(int newInput)
        {
            if (newInput is < 0 or >= 3) return;
            PowerInput = newInput;
            switch (PowerInput)
            {
                case 0:
                    breakageChance = lowPowerBreakageChance;
                    _statusUI.SetPowerInput(lowPowerCost);
                    break;
                case 1:
                    breakageChance = standardBreakageChance;
                    _statusUI.SetPowerInput(MedPowerCost);
                    break;
                case 2:
                    breakageChance = overclockBreakageChance;
                    _statusUI.SetPowerInput(HiPowerCost);
                    break;
            }
        }

        public int GetPowerInput()
        {
            return PowerInput;
        }

        private void Update()
        {
            currentRepairTime += Time.deltaTime * numEngineersAssigned;
        }

        private void Tick()
        {

            if (!isBroken && (breakageChance) / EngineerModifier >= Random.value)
            {
                isBroken = true;
                currentRepairTime = 0;
                _statusUI.OnComponentBreak();
                _warningUI.EnableComponentWarning(true);
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

            if (!isBroken)
            {
                var success = Ship.UsePower(currentPowerCost);
                if (!success && Random.value >= noPowerBreakChance) Break();
                return;
            }
            
            if (currentRepairTime >= repairTime)
            {
                isBroken = false;
                _statusUI.OnComponentFixed();
                _warningUI.EnableComponentWarning(false);
            }
            
        }

        public void Break()
        {
            isBroken = true;
            _statusUI.OnComponentBreak();
            _warningUI.EnableComponentWarning(true);
        }

        public void AddEngineerModifier(float modifier)
        {
            EngineerModifier += modifier;
            numEngineersAssigned++;
        }

        public void RemoveEngineerModifier(float modifier)
        {
            EngineerModifier -= modifier;
            numEngineersAssigned--;
        }
    }
}