using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningUI : MonoBehaviour
{
    [SerializeField] private GameObject componentWarning, powerWarning, orders;
    [SerializeField] private TMP_Text order;
    public void EnableComponentWarning(bool enabled)
    {
        componentWarning.SetActive(enabled);
    }

    public void EnablePowerWarning(bool enabled)
    {
        powerWarning.SetActive(enabled);
    }

    public void SetOrderMessage(string message)
    {
        orders.SetActive(true);
        order.text = message;
    }

    public void DisableOrderMessage()
    {
        orders.SetActive(false);
    }
}
