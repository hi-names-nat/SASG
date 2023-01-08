using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pages : MonoBehaviour
{
    public GameObject[] pages;
    private int currentPage = 0;

    private void Awake()
    {
        pages[0].SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            pages[currentPage].SetActive(false);
            currentPage++;
            pages[currentPage].SetActive(true);
        }
    }
}
