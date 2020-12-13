using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UpdateLap : MonoBehaviour
{
    public GameObject controls;
    public Text controlsText;

    public float currentLap;
    public float maxLaps;

    private float timeInSeconds = 0;
    public TimeSpan timeSpan;
    public string timeText;

    private bool raceGoing = false;

    void OnTriggerEnter()
    {
        if (currentLap == 0)
        {
            currentLap++;
            raceGoing = true;
        }
        else if (currentLap < maxLaps)
        {
            currentLap++;
        }
        else
        {
            raceGoing = false;
            controlsText.text = "Movement: 	WASD / arrows\n" +
                            "Camera: 		SpaceBar\n" +
                            "Handbrake:           X\n" +
                            "Finish!\n" +
                            "Time: " + timeText;
        }
    }

    void Start()
    {
        controls = GameObject.Find("/Canvas/Controls");
        controlsText = controls.GetComponent<Text>();
    }
    void Update()
    {
        if (raceGoing)
            timeInSeconds += Time.deltaTime;

        timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

        controlsText.text = "Movement: 	WASD / arrows\n" +
                            "Camera: 		SpaceBar\n" +
                            "Handbrake:           X\n" +
                            "Lap: " + currentLap + "/" + maxLaps + "\n" +
                            "Time: " + timeText;
    }
}
