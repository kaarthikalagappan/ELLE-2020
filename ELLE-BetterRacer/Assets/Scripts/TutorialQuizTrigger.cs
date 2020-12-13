using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialQuizTrigger : MonoBehaviour
{
    public GameObject question;
    public Text questionText;

    public GameObject answer;
    public Text answerText;

    public GameObject itemBoxes;
    public GameObject itemRow;

    public GameObject previousItemRow;

    List<Pair> pairs = new List<Pair>();

    void Start()
    {
        pairs.Add(new Pair("Select the pumpkin.", "Pumpkin"));

    }

    void OnTriggerEnter(Collider other)
    {
        int index = Random.Range(0, pairs.Count);

        question = GameObject.Find("/Canvas/Question");
        questionText = question.GetComponent<Text>();
        questionText.text = pairs[index].getQuestion();

        answer = GameObject.Find("/Canvas/Answer");
        answerText = answer.GetComponent<Text>();
        answerText.text = pairs[index].getAnswer();

    }
}
