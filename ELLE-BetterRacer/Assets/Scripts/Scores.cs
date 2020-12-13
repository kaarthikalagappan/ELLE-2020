using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Scores : MonoBehaviour
{
    public TMPro.TextMeshProUGUI[] score;
    int startScore = 0;
    public Timer timey;
    private bool firstTouch = true;
    public GameObject answer;
    public Text answerText;
    private string actualAnswer;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startSesh());
    }

    // Update is called once per frame
    void Update()
    {
    }


    [System.Serializable]
    public class sessionJson
    {
        public string sessionID;

        public static sessionJson createFromJson(string jsonString)
        {
            return JsonUtility.FromJson<sessionJson>(jsonString);
        }

        public string getSessionID()
        {
            return sessionID;
        }

    }





    IEnumerator startSesh()
    {
        var date = DateTime.Now;
        string Timeformat = date.Hour.ToString() + ":" + date.Minute.ToString();
        WWWForm form = new WWWForm();
        form.AddField("moduleID", GameManager.moduleID);
        form.AddField("sessionDate", GameManager.sessionDate);
        form.AddField("startTime", Timeformat);
        form.AddField("platform", GameManager.platform);
        UnityWebRequest startSesh = UnityWebRequest.Post("https://endlesslearner.com/api/session", form);
        startSesh.SetRequestHeader("Authorization", "Bearer " + GameManager.token);
        yield return startSesh.SendWebRequest();
        if (startSesh.isNetworkError || startSesh.isHttpError)
        {
            Debug.Log(startSesh.error);
        }
        if(startSesh.responseCode == 201)
        {
            sessionJson session = sessionJson.createFromJson(startSesh.downloadHandler.text);
            //GameManager.sessionID = startSesh.downloadHandler.text.Substring(19).Remove(2);
            GameManager.sessionID = session.getSessionID();
            Debug.Log("Your session id is:" + GameManager.sessionID);
        }
        

    }



    string returnCorrectLapScore()
    {
        if (timey.currentLapTimePlaying.Minutes == 1)
        {
            if (timey.currentLapTimePlaying.Seconds <= 30)
            {
                startScore += 15;
                GameManager.userScore = startScore;
            }
        }
        if (timey.currentLapTimePlaying.Minutes == 1 && timey.currentLapTimePlaying.Seconds <= 45 && timey.currentLapTimePlaying.Seconds >= 30)
        {
            startScore += 6;
            GameManager.userScore = startScore;

        }
        if (timey.currentLapTimePlaying.Minutes == 1 && timey.currentLapTimePlaying.Seconds >= 46)
        {
            startScore += 1;
            GameManager.userScore = startScore;

        }
        return "Score " + startScore.ToString();
    }

    string returnCorrectChoiceScore()
    {
        startScore += 4;
        GameManager.userScore = startScore;
        return "Score " + startScore.ToString();
    }

    void OnTriggerEnter(Collider other)
    {

        answer = GameObject.Find("/Canvas/Answer");
        answerText = answer.GetComponent<Text>();
        actualAnswer = answerText.text;
        //essientially we pass in the name of correct choice and thats how the mechanism will work from a list of correct choices etc
        if (other.gameObject.name == actualAnswer)
        {
            score[0].text = returnCorrectChoiceScore();
        }

        if (other.gameObject.name == "LapTrigger")
        {

            score[0].text = returnCorrectLapScore();
            
        }
    }
}
