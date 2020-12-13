using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetText : MonoBehaviour
{

    public GameObject question;
    public Text questionText;

    void OnTriggerEnter(Collider other)
    {
        question = GameObject.Find("/Canvas/Question");
        questionText = question.GetComponent<Text>();
        questionText.text = "";
    
    }
}
