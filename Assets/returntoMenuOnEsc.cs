using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class returntoMenuOnEsc : MonoBehaviour
{
    private string mainMenuName = "MainMenu";

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            SceneManager.LoadScene(mainMenuName);
    }
}
