using System.Collections;
using System.Collections.Generic;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssignmentSwitch : MonoBehaviour
{
    [SerializeField] private Button selectorButton;
    [SerializeField] private Sprite pressedSprite, unpressedSprite;
    [SerializeField] private Color activeColor, inactiveColor;
    [SerializeField] private EngineerPanel panel;


    [SerializeField] public ShipComponent ShipComponent;

    public void OnButtonClicked()
    {
        Select();
        panel.AssignEngineer(ShipComponent);
    }

    public void Select()
    {
        selectorButton.image.sprite = pressedSprite;
        selectorButton.image.color = inactiveColor;
        selectorButton.interactable = false;
    }

    public void Deselect()
    {
        selectorButton.image.sprite = unpressedSprite;
        selectorButton.image.color = activeColor;
        selectorButton.interactable = true;
    }
}
