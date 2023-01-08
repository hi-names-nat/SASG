using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShipManager : MonoBehaviour
{
    [SerializeField] private float startTimer = 10f;
    private float currentStartTimer = 0;
    
    [Header("General Ship Settings")]
    [SerializeField] private float maxShield = 100;
    [SerializeField] private float shieldFactor = 2f;
    
    [SerializeField] private float maxLifeSupport = 100;
    [SerializeField] private float lifeSupportFactor = 5f;
    
    [Header("Power Settings")]
    
    [SerializeField] private float tickTime = 5f;
    [SerializeField] private int powerPerHarvest = 20;
    [SerializeField] private int powerOverloadThresh = 120;
    [SerializeField, Range(0,1)] private float powerOverloadChance = .4f;
    [SerializeField] private int initialReserves = 20;
    
    [Header("AutoHarvest")]
    [SerializeField, Range(0, 1)] private float autoHarvestFailChance = .4f;
    [SerializeField] private float autoHarvestRepairTime = 30f;
    [SerializeField, ReadOnly] private float currentAutoHarvestRepairTime = 0;



    

    [Header("UI")]
    [SerializeField] private HarvestPanel harvestPanel;
    [SerializeField] private PowerReservesPanel ReservesPanel;
    [SerializeField] private PanelUsageBar usageBar;
    [SerializeField] private Image ReservesWarningLight;
    [SerializeField] private Color unlitColor, DangerColor;
    [SerializeField] private EndGameUI _endGameUI;
    [SerializeField] private WarningUI _warningUI;


    [Header("Orders / Combat")]
    [SerializeField] private OrdersManager ordersManager;
    [Range(0, 1)] public float shotChancePerTick = .1f;

    [SerializeField, ReadOnly] float _currentLifeSupport;
    [SerializeField, ReadOnly] private float _currentShield;
    private int lastPower = 0;

    [SerializeField] private MainUIManager mainUI;

    [SerializeField] AudioSource explosion;

    public event Action Tick;
    
    private int _reactorUsage;

    private int _currentPower;

    private int _powerUsedThisTick;

    private bool _autoHarvest = true;
    private float _currentTime = 0;

    private void Awake()
    {
        _currentPower = initialReserves;
        ReservesPanel.SetPowerReserves(_currentPower);
        _currentShield = maxShield;
        Tick += DecideHasBeenShot;
        _currentLifeSupport = maxLifeSupport;
        _currentTime = tickTime;
        ReservesWarningLight.color = unlitColor;
        UpdateMainUI();
        Tick += UpdateMainUI;
    }

    public void HarvestPower(int NumToAdd)
    {
        if (_currentPower >= powerOverloadThresh)
        {
            if (Random.value >= powerOverloadChance)
            {
                _currentPower = 0;
                //Power overloaded. Do stuff.

            }
        }
        _currentPower += NumToAdd;
        ReservesPanel.SetPowerReserves(_currentPower);
    }
    
    public void BreakAutoHarvest()
    {
        _autoHarvest = false;
        harvestPanel.AutoHarvestBroke();
        _warningUI.EnablePowerWarning(true);
    }

    public void RepairAutoHarvest()
    {
        _autoHarvest = true;
        harvestPanel.AutoHarvestFixed();
        _warningUI.EnablePowerWarning(false);
    }
    
    private void Update()
    {
        if (currentStartTimer < startTimer)
        {
            currentStartTimer += Time.deltaTime;
            return;
        }
        _currentTime += Time.deltaTime;
        if (!_autoHarvest)
        {
            currentAutoHarvestRepairTime += Time.deltaTime;
            if (currentAutoHarvestRepairTime >= autoHarvestRepairTime)
            {
                RepairAutoHarvest();

            }
        }
        if (_currentTime < tickTime) return;
        if (_currentPower >= powerOverloadThresh - 30)
        {
            ReservesWarningLight.color = DangerColor;
            _warningUI.EnablePowerWarning(true);
        }   
        else
        {
            ReservesWarningLight.color = unlitColor;
            _warningUI.EnablePowerWarning(false);
        }
        _currentTime = 0;
        if (_autoHarvest) HarvestPower(powerPerHarvest);
        ReservesPanel.SetPowerReserves(_currentPower);
        print("Tick");
        Tick?.Invoke();

        switch (lastPower - _currentPower)
        {
            case <= 10:
                usageBar.SetUsage(0);
                break;
            case > 10 and < 20:
                usageBar.SetUsage(1);
                break;
            case >= 20 and < 25:
                usageBar.SetUsage(2);
                break;
            case >= 25 and < 30:
                usageBar.SetUsage(3);
                break;
            case >= 30 and < 40:
                usageBar.SetUsage(4);
                break;
        }
        
        lastPower = _currentPower;
        
        
    }

    private void UpdateMainUI()
    {
        mainUI.UpdateUI(_currentShield, _currentLifeSupport);
    }

    public void DecideHasBeenShot()
    {
        if (Random.value > shotChancePerTick) return;
        
        //has been shot
        if (Random.value <= autoHarvestFailChance)
        {
            Shot();
        }
        explosion.Play();
        _currentShield -= 15;
    }

    public void Shot()
    {
        BreakAutoHarvest();
        if (!explosion.isPlaying)
            explosion.Play();

    }

    public bool UsePower(int amount)
    {
        if (_currentPower < amount)
        {
            // power overload. Break the reactor, half all power output.
            _currentPower = 0;
            return false;
        }
        _currentPower -= amount;
        return true;

    }

    public void TickLifeSupport(bool broken)
    {
        if (broken)
        {
            UpdateMainUI();
            _currentLifeSupport -= lifeSupportFactor;
            if (_currentLifeSupport <= 0)
            {
                //dead.
                EndGame(false);
            }
        }
        else
        {
            _currentLifeSupport = Mathf.Min(_currentLifeSupport + lifeSupportFactor, 100);
        }
    }

    public void TickShield(bool broken)
    {
        if (broken)
        {
            _currentShield -= shieldFactor;
            UpdateMainUI();
            if (_currentShield <= 0)
            {
                //dead.
                EndGame(false);
            }
        }
        else
        {
            _currentShield = Mathf.Min(shieldFactor + _currentShield, 100);

        }
    }

    public void EndGame(bool playerVictory)
    {
        if (playerVictory) _endGameUI.EndGamePlayerVictory();
        else _endGameUI.EndGamePlayerDefeat();
    }
}
