using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Spin : MonoBehaviour
{
    public GameObject car;
    public CarBehaviour cb;
    public GameObject question;
    public Text questionText;
    public GameObject destroyChoice;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        car = GameObject.Find("/Vehicles/SportsCar");
        cb = car.GetComponent<CarBehaviour>();
        cb.TriggerDamageOn();
        destroyChoice.SetActive(false);
    }
}
