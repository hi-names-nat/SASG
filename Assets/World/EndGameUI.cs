using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private Canvas VictoryCanvas;
    [SerializeField] private Canvas FailureCanvas;
    public void EndGamePlayerVictory()
    {
        //If add anim, replace it in here.
        VictoryCanvas.enabled = true;
        Destroy(FailureCanvas.gameObject);
    }

    public void EndGamePlayerDefeat()
    {
        FailureCanvas.enabled = true;
        
    }
}
