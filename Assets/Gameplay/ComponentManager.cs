using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    [SerializeField] private ShipComponent Component;


    public void SetComponentPowerInput(int Power)
    {
        Component.SetPowerInput(Power);
    }
}
