using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour
{

    public string chosenAnswer;
    private string actualAnswer;

    public GameObject question;
    public Text questionText;

    public GameObject answer;
    public Text answerText;

    public GameObject car;
    public CarBehaviour cb;

    public GameObject powerupArray;
    private float chosenPowerupIndex;

    void OnTriggerEnter(Collider other)
    {
        question = GameObject.Find("/Canvas/Question");
        questionText = question.GetComponent<Text>();

        answer = GameObject.Find("/Canvas/Answer");
        answerText = answer.GetComponent<Text>();
        actualAnswer = answerText.text;

        car = GameObject.Find("/Vehicles/SportsCar");
        cb = car.GetComponent<CarBehaviour>();

        powerupArray = GameObject.Find("/Canvas/PowerupArray");
        
        //powerupArray.transform.GetChild(3).gameObject.SetActive(false);

        Debug.Log("Actual: " + actualAnswer + "\nChosen: " + chosenAnswer);
        if (actualAnswer.Equals(chosenAnswer))
        {
            questionText.text = "Correto!";
            
        
        }
        else
        {
            if (CarBehaviour.isProtected)
            {
                questionText.text = "Salvou!";
                CarBehaviour.isProtected = false;
            }
            else
            {
                cb.TriggerDamageOn();
                questionText.text = "Incorreto.";
            }
        }


        this.gameObject.SetActive(false);
    }
}
