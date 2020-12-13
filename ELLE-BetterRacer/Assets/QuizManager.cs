using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public GameObject powerupArray;
    
    // Start is called before the first frame update
    void Start()
    {
        powerupArray = GameObject.Find("/Canvas/PowerupArray");
        
        powerupArray.transform.GetChild(0).gameObject.SetActive(false);
        powerupArray.transform.GetChild(1).gameObject.SetActive(false);
        powerupArray.transform.GetChild(2).gameObject.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
