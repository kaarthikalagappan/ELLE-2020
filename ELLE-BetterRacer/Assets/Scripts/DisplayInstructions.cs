using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayInstructions : MonoBehaviour
{

    public GameObject uiObject;
    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
        continueButton.SetActive(false);

    }

    void OnTriggerEnter(Collider player)
    {
        continueButton.SetActive(true);
        if (player.name == "SportsCarChassis")
        {
            uiObject.SetActive(true);
            StartCoroutine("WaitForSec");
        }

    }

    void OnTriggerExit(Collider player)
    {
        Destroy(uiObject);

    }

    IEnumerator WaitForSec()
	{
		yield return new WaitForSeconds(1);
        Time.timeScale = 0;

    }

}
