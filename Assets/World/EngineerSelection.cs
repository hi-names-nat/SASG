using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EngineerSelection : MonoBehaviour
{
    [SerializeField] private TMP_Text Name;
    [SerializeField] private Button selectorButton;
    [SerializeField] private Sprite pressedSprite, unpressedSprite;
    [SerializeField] private Color activeColor, inactiveColor;
    [SerializeField] private Engineer thisEngineer;
    [SerializeField] private EngineerPanel panel;

    private void Awake()
    {
        Name = GetComponentInChildren<TMP_Text>();
        selectorButton = GetComponentInChildren<Button>();
        UpdateEngineerName();
    }

    public void UpdateEngineerName()
    {
        Name.text = thisEngineer.Name;
    }

    public void OnButtonPressed()
    {
        panel.SelectEngineer(thisEngineer, this);
    }

    public void OnSelect()
    {
        selectorButton.image.sprite = pressedSprite;
        selectorButton.image.color = inactiveColor;
        selectorButton.interactable = false;
    }

    public void OnDeselect()
    {
        selectorButton.image.sprite = unpressedSprite;
        selectorButton.image.color = activeColor;
        selectorButton.interactable = true;
    }
}
