using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockTimer : MonoBehaviour
{

    //Start time timer
    [SerializeField] float startTime;

    //current time
    float currentTime;

    // has time started?
    bool timerStarted = false;

    // Ref var for my UI text
    [SerializeField] TMP_Text timerText;

    private void Start()
    {
        currentTime = startTime;
        timerText.text = currentTime.ToString();
    }

    private void Update()
    {
        timerStarted = true;
        currentTime += Time.deltaTime;


        if (currentTime > 0)
        {
            //Debug.Log("Timer Started");

        }
        timerText.text = currentTime.ToString("f0");
    }
}