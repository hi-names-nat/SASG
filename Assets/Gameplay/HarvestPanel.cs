using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class HarvestPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text NotBroken, Broken;
    [SerializeField] private Button ManualButton;
    [SerializeField] private Color DisabledColor, EnabledColor;
    [SerializeField] private Sprite PressedImage, UnpressedImage;


    private void Awake()
    {
        AutoHarvestFixed();
    }

    public void AutoHarvestBroke()
    {
        NotBroken.gameObject.SetActive(false);
        Broken.gameObject.SetActive(true);
        ManualButton.interactable = true;
        ManualButton.image.sprite = UnpressedImage;
        ManualButton.image.color = EnabledColor;
    }

    public void AutoHarvestFixed()
    {
        NotBroken.gameObject.SetActive(true);
        Broken.gameObject.SetActive(false);
        ManualButton.interactable = false;
        ManualButton.image.sprite = PressedImage;
        ManualButton.image.color = DisabledColor;
    }
}
