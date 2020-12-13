using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class NewDestroy : MonoBehaviour
{
    public GameObject actualGameObject;
    private string actualAnswer;

    public GameObject question;
    public Text questionText;

    public GameObject answer;
    public Text answerText;

    public GameObject car;
    public CarBehaviour cb;

    public GameObject powerupArray;
    private float chosenPowerupIndex;

    IEnumerator setLoggedAnswerCorrect()
    {
        WWWForm form = new WWWForm();
        form.AddField("questionID", GameManager.questionID);
        form.AddField("termID", GameManager.termID);
        form.AddField("sessionID", GameManager.sessionID);
        form.AddField("correct", "1");
        UnityWebRequest startSesh = UnityWebRequest.Post("https://endlesslearner.com/api/loggedanswer", form);
        startSesh.SetRequestHeader("Authorization", "Bearer " + GameManager.token);
        yield return startSesh.SendWebRequest();
        if (startSesh.isNetworkError || startSesh.isHttpError)
        {
            Debug.Log(startSesh.error);
        }
        if (startSesh.responseCode == 205)
        {
            Debug.Log("Saved your logged answer");
        }

    }

    IEnumerator setLoggedAnswerWrong()
    {
        WWWForm form = new WWWForm();
        form.AddField("questionID", GameManager.questionID);
        form.AddField("termID", GameManager.termID);
        form.AddField("sessionID", GameManager.sessionID);
        form.AddField("correct", "0");
        UnityWebRequest startSesh = UnityWebRequest.Post("https://endlesslearner.com/api/loggedanswer", form);
        startSesh.SetRequestHeader("Authorization", "Bearer " + GameManager.token);
        yield return startSesh.SendWebRequest();
        if (startSesh.isNetworkError || startSesh.isHttpError)
        {
            Debug.Log(startSesh.error);
        }
        if (startSesh.responseCode == 205)
        {
            Debug.Log("Saved your logged answer");
        }

    }


    void OnTriggerEnter(Collider other)
    {
        string chosenAnswer = actualGameObject.name;
        question = GameObject.Find("/Canvas/Question");
        questionText = question.GetComponent<Text>();

        answer = GameObject.Find("/Canvas/Answer");
        answerText = answer.GetComponent<Text>();
        actualAnswer = answerText.text;

        car = GameObject.Find("/Vehicles/SportsCar");
        cb = car.GetComponent<CarBehaviour>();

        powerupArray = GameObject.Find("/Canvas/PowerupArray");

        powerupArray.transform.GetChild(3).gameObject.SetActive(false);

        Debug.Log("Actual: " + actualAnswer + "\nChosen: " + chosenAnswer);
        if (actualAnswer.Equals(chosenAnswer))
        {
            questionText.text = "Correto! Right Answer: " + GameManager.correctAnswerToShow;
            CarBehaviour.isProtected = false;
            powerupArray.transform.GetChild(0).gameObject.SetActive(false);
            powerupArray.transform.GetChild(1).gameObject.SetActive(false);
            powerupArray.transform.GetChild(2).gameObject.SetActive(false);
            powerupArray.transform.GetChild(3).gameObject.SetActive(false);
            StartCoroutine(setLoggedAnswerCorrect());
            GameManager.totalcorrect++;

            int chosenPowerupIndex = UnityEngine.Random.Range(0, 3);
            powerupArray.transform.GetChild(chosenPowerupIndex).gameObject.SetActive(true);
            cb.SetPowerup(chosenPowerupIndex + 1);

        }
        else
        {
            if (CarBehaviour.isProtected)
            {
                questionText.text = "Saved! Correct Answer: " + GameManager.correctAnswerToShow; 
                CarBehaviour.isProtected = false;
            }
            else
            {
                cb.TriggerDamageOn();
                questionText.text = "Wrong. Correct Answer: " + GameManager.correctAnswerToShow;
            }
            StartCoroutine(setLoggedAnswerWrong());
            GameManager.totalincorrect++;

        }


        this.gameObject.SetActive(false);
    }
}
