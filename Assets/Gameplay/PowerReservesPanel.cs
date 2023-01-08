using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerReservesPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPowerReserves(float value)
    {
        _text.text = value.ToString();
    }
}
