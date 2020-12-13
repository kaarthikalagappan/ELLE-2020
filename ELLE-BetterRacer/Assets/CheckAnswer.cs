using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnswer : MonoBehaviour
{
    public string chosenAnswer = "";
    public string actualAnswer = "";
    public bool readyForAnswer = false;
    // Update is called once per frame
    void Update()
    {
        if (readyForAnswer) {
            Debug.Log("Ready for answer.");
            readyForAnswer = false;


        }
    }
}
