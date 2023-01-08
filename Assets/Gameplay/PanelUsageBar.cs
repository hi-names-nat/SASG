using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUsageBar : MonoBehaviour
{
    [SerializeField] private Image lowest, low, medium, high, highest;

    [SerializeField] private Color OKColor, WarnColor, OverLoadColor, UnusedColor;



    public void SetUsage(int lights)
    {
        lowest.color = UnusedColor;
        low.color = UnusedColor;
        medium.color = UnusedColor;
        high.color = UnusedColor;
        highest.color = UnusedColor;
        switch (lights)
        {
            case 4:
                highest.color = OverLoadColor;
                goto case 3;
            case 3:
                high.color = WarnColor;
                goto case 2;
            case 2:
                medium.color = WarnColor;
                goto case 1;
            case 1:
                low.color = OKColor;
                goto case 0;
            case 0:
                lowest.color = OKColor;
                goto default;
            default:
                return;
                
        }
    }


}
