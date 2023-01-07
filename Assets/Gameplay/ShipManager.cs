using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ShipManager : MonoBehaviour
{
    [SerializeField] private float tickTime = 5f;
    [SerializeField] private int powerPerHarvest = 20;
    [SerializeField] private int powerOverloadThresh = 120;
    [SerializeField, Range(0,1)] private float powerOverloadChance = .4f;
    [SerializeField, Range(0, 1)] private float autoHarvestFailChance = .4f;
    [Range(0, 1)] public float shotChancePerTick = .1f;
    [SerializeField] private float maxShield = 100;
    [SerializeField] private float shieldFactor = 2f;
    [SerializeField] private float maxLifeSupport = 100;
    [SerializeField] private float lifeSupportFactor = 5f;
    [FormerlySerializedAs("_ordersManager")] [SerializeField] private OrdersManager ordersManager;
    private float _currentLifeSupport;
    private float _currentShield;

    public event Action Tick;
    
    private int _reactorUsage;

    private int _currentPower;

    private bool _autoHarvest = true;
    private float _currentTime = 0;

    private void Awake()
    {
        _currentShield = maxShield;
        Tick += DecideHasBeenShot;
        _currentLifeSupport = maxLifeSupport;
    }

    private void HarvestPower()
    {
        if (_currentPower >= powerOverloadThresh)
        {
            if (Random.value <= powerOverloadChance)
            {
                _currentPower = 0;
                //Power overloaded. Do stuff.

            }
        }
        _currentPower += powerPerHarvest;
    }

    public void BreakAutoHarvest()
    {
        _autoHarvest = false;
        //etc..
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (!(_currentTime <= tickTime)) return;
        
        _currentTime = 0;
        if (_autoHarvest) HarvestPower();
        Tick.Invoke();
    }

    public void DecideHasBeenShot()
    {
        if (!(Random.value <= shotChancePerTick)) return;
        
        //has been shot
        if (Random.value <= autoHarvestFailChance)
        {
            BreakAutoHarvest();
        }
    }

    public void TickLifeSupport(bool broken)
    {
        if (broken)
        {
            _currentLifeSupport -= lifeSupportFactor;
            if (_currentLifeSupport <= 0)
            {
                //dead.
                EndGame(false);
            }
        }
        else
        {
            _currentLifeSupport += lifeSupportFactor;
        }
    }

    public void TickShield(bool broken)
    {
        if (broken)
        {
            _currentShield -= shieldFactor;
            if (_currentShield <= 0)
            {
                //dead.
                EndGame(false);
            }
        }
        else
        {
            _currentShield += shieldFactor;
        }
    }
    
    public void ComponentUpdated(ComponentTypes type, int value)
    {
        ordersManager.ComponentUpdated(type, value);
    }

    public void EndGame(bool playerVictory)
    {
        
    }
}
