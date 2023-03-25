using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int trialNum;
    public string trialName;
    public List<string> trials;

    private string sceneName;
    public float trialTimer = 0;
    private bool timerIsActive = true;

    bool pacmanEaten = false;

    // Start is called before the first frame update
    void Start()
    {
        trialNum = GlobalControl.Instance.trialNum;
        trialName = GlobalControl.Instance.trialName;
        trials = GlobalControl.Instance.trials;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsActive)
        {
            trialTimer += Time.deltaTime;
        }
    }


    public void SaveGame()
    {
        GlobalControl.Instance.trialNum = trialNum;
        GlobalControl.Instance.trialName = trialName;
        GlobalControl.Instance.trials = trials;
    }


    public void pacmanDead()
    {
        pacmanEaten = true;
        ResetRound();
    }


    public void ResetRound()
    {

        if ((Timer.currentTime >= Timer.inverseTime) || pacmanEaten)
        {

            trialNum ++;

            int tempTrialNum = trialNum; //so that tinylytics doesn't mess with trialNum
            string tempTrialName = trialName;

            //Log all Tinylytics at Round End

            Debug.Log("Round Over!");
            SaveGame();
            newTrial();

            timerIsActive = false;
            pacmanEaten = false;

        }

    }

    void newTrial()
    {

        if (trialNum < trials.Count)
        {
            trialName = trials[trialNum];
            SaveGame();

            sceneName = "Interstitial"; //this name is used in the Coroutine, which is basically just a pause timer for 3 seconds.

            StartCoroutine(WaitForSceneLoad());
        }
        else
        {
            endGame();

        }

    }

    void endGame()
    {
        //if you want to know how lond the entire set of trials took, you can add your tinyLytics call here
        sceneName = "ending"; //this name is used in the Coroutine, which is basically just a pause timer for 3 seconds.
        StartCoroutine(WaitForSceneLoad());

    }

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    
}
