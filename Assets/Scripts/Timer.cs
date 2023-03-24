using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 30;
    public static float inverseTime = 30;
    public static float currentTime;
    bool timerIsRunning = false;

    GameManager ResetRound;
    public TextMeshProUGUI TimeLeft;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;

        // Grab access to GameManager in order to call ResetRound() function
        ResetRound = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                currentTime = exportTime();

            }
            else
            {
                Debug.Log("Time has run out! " + currentTime);
                timeRemaining = 0;
                timerIsRunning = false;
                ResetRound.ResetRound();
            }
        }
    }

    public float exportTime()
    {
        return inverseTime - timeRemaining;
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimeLeft.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
