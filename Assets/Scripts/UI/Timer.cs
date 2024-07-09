using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerTimer : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI timerText;
    public float elapsedTime;
    bool timerStopped = false;
    public float finalTime;

    private void Update ( ) {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);        
    }

    public void StopTimer ( ) {
        timerStopped = true;
        finalTime = elapsedTime;
    }

    // Optional method to retrieve the final time
    public float GetFinalTime ( ) {
        if(!timerStopped) {
            Debug.LogWarning("Timer is still running. GetFinalTime() might not return the final elapsed time.");
        }
        return finalTime;
    }

}
