using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasSwitch : MonoBehaviour
{

    public Canvas oldCanvas, newCanvas;
    public void SwitchCanvas()
    {
        oldCanvas.gameObject.SetActive(false);
        newCanvas.gameObject.SetActive(true);

    }
}
