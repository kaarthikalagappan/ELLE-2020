using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text currentLap;
    public Text bestLap;
    public Text Lap;

    private bool finished = false;
    public static bool lapfinish;

    private float elapsedTime;
    private TimeSpan timePlaying;

    private float bestLapTime;

    private float currentLapTime;
    public TimeSpan currentLapTimePlaying;

    private int lap;
    private int improvisedTimer;

    public GameObject finishPanel;
    public Text incorrectText;
    public Text correctText;
    public Text TotalTimeText;
    public Text moduleText;

    private int totalLaps;


    void Start()
    {
       
        elapsedTime = 0f;
        currentLapTime = 0f;
        bestLapTime = 100000000f;
        lapfinish = false;
        lap = -1;
        finishPanel.SetActive(false);
        string lapp = PlayerPrefs.GetString("lapAmount");
        totalLaps = Int32.Parse(lapp);


    }

    private void OnTriggerEnter(Collider other)
    {
        lap++;
        if (lap >= 1)
        {
            lapfinish = true;

        }
        if(lap >= 0)
        {
            string lapp = PlayerPrefs.GetString("lapAmount");
            Lap.text = "LAP: " + (lap+1).ToString() + "/" + lapp;
        }


    }

    // Update is called once per frame
    void Update()
    {
        //yield return new WaitForSeconds(3f);
        if (finished)
            return;
        
        if(improvisedTimer > 200)
        {
            StartCoroutine("timerStart");
        }
        else
        {
            improvisedTimer++;
        }

    }

    public void Finnish()
    {
        finished = true;
        timerText.color = Color.yellow;
        StartCoroutine(endSesh());
        StartCoroutine(postGameLog());

    }

    IEnumerator endSesh()
    {
        var date = DateTime.Now;
        string Timeformat = date.Hour.ToString() + ":" + date.Minute.ToString();
        WWWForm form = new WWWForm();
        form.AddField("sessionID", GameManager.sessionID);
        form.AddField("endTime", Timeformat);
        form.AddField("playerScore", GameManager.userScore.ToString());
        UnityWebRequest endSesh = UnityWebRequest.Post("https://endlesslearner.com/api/endsession", form);
        endSesh.SetRequestHeader("Authorization", "Bearer " + GameManager.token);
        yield return endSesh.SendWebRequest();
        if (endSesh.isNetworkError || endSesh.isHttpError)
        {
            Debug.Log(endSesh.error);
            Debug.Log(endSesh.downloadHandler.text);
        }
        if (endSesh.responseCode == 201)
        {
            Debug.Log("Your session is donezo");
        }
    }



    IEnumerator postGameLog()
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", GameManager.userID);
        form.AddField("moduleID", GameManager.moduleID);
        form.AddField("correct", GameManager.totalcorrect.ToString());
        form.AddField("incorrect", GameManager.totalincorrect.ToString());
        form.AddField("platform", GameManager.platform);
        form.AddField("time", "2019-02-18 11:15:45");
        UnityWebRequest startSesh = UnityWebRequest.Post("https://endlesslearner.com/api/gamelog", form);
        startSesh.SetRequestHeader("Authorization", "Bearer " + GameManager.token);
        yield return startSesh.SendWebRequest();
        if (startSesh.isNetworkError || startSesh.isHttpError)
        {
            Debug.Log(startSesh.error);
            Debug.Log(startSesh.downloadHandler.text);
        }
        if (startSesh.responseCode == 206)
        {
            Debug.Log("Your session is donezo");
        }
    }

    IEnumerator timerStart()
    {
        elapsedTime += Time.deltaTime;
        currentLapTime += Time.deltaTime;

        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        currentLapTimePlaying = TimeSpan.FromSeconds(currentLapTime);


        string timeStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
        timerText.text = timeStr;

        timeStr = "Current Lap: " + currentLapTimePlaying.ToString("mm':'ss'.'ff");
        currentLap.text = timeStr;


        if (lapfinish)
        {
            if (bestLapTime > currentLapTime)
            {
                bestLap.gameObject.SetActive(true);
                timeStr = "Best Lap: " + currentLapTimePlaying.ToString("mm':'ss'.'ff");
                currentLapTimePlaying = currentLapTimePlaying.Subtract(currentLapTimePlaying);
                bestLap.text = timeStr;
                bestLapTime = currentLapTime;
            }
            currentLapTime = 0f;

            //finish mechanism here will set to 3
            if (lap == totalLaps)
            {
                Finnish();
                Time.timeScale = 0;
                correctText.text = GameManager.totalcorrect.ToString();
                incorrectText.text = GameManager.totalincorrect.ToString();
                TotalTimeText.text = timePlaying.ToString("mm':'ss'.'ff"); ;
                moduleText.text = GameManager.moduleName;
                finishPanel.SetActive(true);


            }
            lapfinish = false;
        }

        yield return 1;
    
    }


}
