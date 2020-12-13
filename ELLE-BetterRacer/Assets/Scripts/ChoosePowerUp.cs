using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePowerUp : MonoBehaviour
{
    public int chosenPowerUp;
    public GameObject destroyChoice;
    public GameObject car;
    public CarBehaviour cb;
    public GameObject powerupArray;


    void OnTriggerEnter(Collider other)
    {
        car = GameObject.Find("/Vehicles/SportsCar");
        cb = car.GetComponent<CarBehaviour>();


        powerupArray = GameObject.Find("/Canvas/PowerupArray");
        powerupArray.transform.GetChild(0).gameObject.SetActive(false);
        powerupArray.transform.GetChild(1).gameObject.SetActive(false);
        powerupArray.transform.GetChild(2).gameObject.SetActive(false);
        powerupArray.transform.GetChild(chosenPowerUp).gameObject.SetActive(true);
        cb.SetPowerup(chosenPowerUp + 1);
        Destroy(destroyChoice);

    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
