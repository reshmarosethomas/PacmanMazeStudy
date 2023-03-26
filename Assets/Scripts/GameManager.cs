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
    private int score = 0;
    public TextMeshProUGUI ScoreTxt;

    public string blinkyDists;
    public string inkyDists;
    public string pinkyDists;
    public string clydeDists;

    float timeTaken = 0f;

    public int inFullScreen;

    void Awake()
    {
        inFullScreen = 1;
    }

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

        if(Screen.fullScreen==false)
        {
            inFullScreen = 0;
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

    public void addToScore()
    {
        score++;
        ScoreTxt.text = score.ToString();
    }


    public void ResetRound()
    {

        if ((Timer.currentTime >= Timer.inverseTime) || pacmanEaten)
        {

            trialNum ++;

            int tempTrialNum = trialNum; //so that tinylytics doesn't mess with trialNum
            string tempTrialName = trialName;

            //Log all Tinylytics at Round End

            //1. Time Taken for Round
            if (Timer.currentTime > Timer.inverseTime) timeTaken = Timer.inverseTime;
            else timeTaken = Timer.currentTime;
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "TimeTaken", timeTaken.ToString());

            //2. Log Score
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "PacdotsCollected", score.ToString());

            //3. Distances from Ghosts over Time
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "BlinkyDistances", blinkyDists);
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "InkyDistances", inkyDists);
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "PinkyDistances", pinkyDists);
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "ClydeDistances", clydeDists);

            //4. No of Times Pacman passed through a door (in C2, C3)


            //5. ExitedFullScreen?
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + tempTrialName + "_" + tempTrialNum.ToString() + "_" + "InFullScreen", inFullScreen.ToString());

            //6.Log Trial End
            Tinylytics.AnalyticsManager.LogCustomMetric(SaveProlificID.prolificID + "_" + trialName + "_" + tempTrialNum.ToString() + "_" + "TrialEndTime", "End " + System.DateTime.Now);

            Debug.Log(blinkyDists);
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
