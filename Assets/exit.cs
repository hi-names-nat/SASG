using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class exit : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }


    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame) Exit();
    }
}
