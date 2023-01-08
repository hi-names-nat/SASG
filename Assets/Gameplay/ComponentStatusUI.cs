using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponentStatusUI : MonoBehaviour
{
    [SerializeField] private Color OkColor, ErrorColor;
    [SerializeField] private Image StatusImage;
    [SerializeField] private TMP_Text NumberIndicator;

    private void Awake()
    {
    }

    public void OnComponentBreak()
    {
        StatusImage.color = ErrorColor;
    }

    public void OnComponentFixed()
    {
        StatusImage.color = OkColor;
    }

    public void SetPowerInput(int value)
    {
        NumberIndicator.text = value.ToString();
    }
    
    
}
