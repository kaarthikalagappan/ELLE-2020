using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using System.Collections;
using System.Collections.Generic;

public class CarBehaviour : MonoBehaviour
{

    public GameObject car;

    public bool isDamaged;

    public float spinTime;

    private float timeLeft;

    
    private float currentPowerup = 0;

    public GameObject powerupArray;

    public static GameObject currentItemRow;

    public static bool isProtected = false;

    //here i play
    WheelCollider [] m_Wheels;

    protected JoyButton button;
    private int burnoutsecs;

    // Start is called before the first frame update
    void Start()
    {
        car = GameObject.Find("/Vehicles/SportsCar");
        timeLeft = spinTime;
        button = FindObjectOfType<JoyButton>();
        string temp = PlayerPrefs.GetString("burnoutsecs");
        if(temp.Equals(""))
        {
            Debug.Log("yerr");
            temp = "2";
        }
        burnoutsecs = Int32.Parse(temp);
        powerupArray = GameObject.Find("/Canvas/PowerupArray");
    }

    // Update is called once per frame
    void Update()
    {
        //Maybe this is better than the spin?
        if (isDamaged)
        {
            ////car.transform.Rotate(new Vector3(0f, 1f, 0f));
            car.GetComponent<Rigidbody>().drag = 1;
        }
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) {
            TriggerDamageOff();
        }

        if (button.Pressed)
        {
            button.Pressed = false;
            Debug.Log("df");
            powerupArray.transform.GetChild(0).gameObject.SetActive(false);
            powerupArray.transform.GetChild(1).gameObject.SetActive(false);
            powerupArray.transform.GetChild(2).gameObject.SetActive(false);
            powerupArray.transform.GetChild(3).gameObject.SetActive(false);
            Debug.Log("Current powerup: " + currentPowerup);
            switch (currentPowerup)
            {
                case 0:
                    break;
                case 1:
                    currentPowerup = 0;
                    int deleteIndex = UnityEngine.Random.Range(0, 3);
                    string currentAnswer = GameObject.Find("/Canvas/Answer").GetComponent<Text>().text;
                    Debug.Log("Selected item for deletion: " + currentItemRow.transform.GetChild(deleteIndex));
                    Debug.Log("Current answer: " + currentAnswer);
                    while (String.Compare(currentItemRow.transform.GetChild(deleteIndex).name, currentAnswer) == 0)
                    {
                        deleteIndex = UnityEngine.Random.Range(0, 3);
                        Debug.Log("New selected item for deletion: " + currentItemRow.transform.GetChild(deleteIndex));
                    }
                    
                    switch (deleteIndex)
                    {
                        case 0:
                            currentItemRow.transform.GetChild(0).gameObject.SetActive(false);
                            currentItemRow.transform.GetChild(1).gameObject.SetActive(true);
                            currentItemRow.transform.GetChild(2).gameObject.SetActive(true);
                            break;
                        case 1:
                            currentItemRow.transform.GetChild(0).gameObject.SetActive(true);
                            currentItemRow.transform.GetChild(1).gameObject.SetActive(false);
                            currentItemRow.transform.GetChild(2).gameObject.SetActive(true);
                            break;
                        case 2:
                            currentItemRow.transform.GetChild(0).gameObject.SetActive(true);
                            currentItemRow.transform.GetChild(1).gameObject.SetActive(true);
                            currentItemRow.transform.GetChild(2).gameObject.SetActive(false);
                            break;
                        default:
                            break;
                    }

                    break;
                case 2:
                    Pause(3);
                    currentPowerup = 0;
                    break;
                case 3:
                    isProtected = true;
                    currentPowerup = 0;
                    powerupArray.transform.GetChild(3).gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    public void TriggerDamageOn()
    {
        timeLeft = burnoutsecs;
        isDamaged = true;
    }

    public void TriggerDamageOff()
    {
        timeLeft = burnoutsecs;
        isDamaged = false;
        car.GetComponent<Rigidbody>().drag = 0;

    }

    public void SetPowerup(float n)
    {
        currentPowerup = n;
    }

    public void Pause(float delta)
    {
        float lastInterval = Time.realtimeSinceStartup;
        Time.timeScale = 0;
        while (Time.realtimeSinceStartup - lastInterval < delta) {};
        Time.timeScale = 1;

    }
}
