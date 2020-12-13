using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string userID;
    public static string moduleID;
    public static string token;


    //session variables
    public static string sessionID;
    public static string sessionDate;
    public static string totalScore;
    public static string startTime;
    public static string endTime;
    public static string platform;
    public static string moduleName;


    //logging variables
    public static string questionID;
    public static string termID;
    public static char correct;

    //final session variables;
    public static int totalcorrect;
    public static int totalincorrect;
    public static string fullDate;

    public static string correctAnswerToShow;

    public static int userScore;

    public static bool enableSpin;



    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(userID);
        //Debug.Log(moduleID);
        //Debug.Log(token);
        platform = "mb";
        var date = DateTime.Now;
        sessionDate = date.ToString("d");
        fullDate = date.ToString("d");
        totalcorrect = 0;
        totalincorrect = 0;
        enableSpin = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
