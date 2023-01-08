using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class ComponentButtonGroup : MonoBehaviour
{
    [SerializeField] private Button LowPowerButton, NormalButton, HighPowerButton;
    [SerializeField] private Sprite unpressedSprite, PressedSprite;

    private ShipComponent _shipComponent;

    private void Awake()
    {
        _shipComponent = GetComponent<ShipComponent>();
        MedPowerPressed();
    }

    private void AnyButtonPressed()
    {
        // print("Button Pressed.");
    }

    public void LowPowerPressed()
    {
        LowPowerButton.image.sprite = PressedSprite;
        NormalButton.image.sprite = unpressedSprite;
        HighPowerButton.image.sprite = unpressedSprite;
        _shipComponent.SetPowerInput(0);
        AnyButtonPressed();
    }
    
    public void MedPowerPressed()
    {
        LowPowerButton.image.sprite = unpressedSprite;
        NormalButton.image.sprite = PressedSprite;
        HighPowerButton.image.sprite = unpressedSprite;
        _shipComponent.SetPowerInput(1);
        AnyButtonPressed();
    }
    
    public void HiPowerPressed()
    {
        LowPowerButton.image.sprite = unpressedSprite;
        NormalButton.image.sprite = unpressedSprite;
        HighPowerButton.image.sprite = PressedSprite;
        _shipComponent.SetPowerInput(2);
        AnyButtonPressed();
    }
}
