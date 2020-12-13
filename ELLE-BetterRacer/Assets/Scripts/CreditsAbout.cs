using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsAbout : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject aboutPanel;

    public void clickCredit()
    {
        creditsPanel.gameObject.SetActive(true);
    }

    public void closeCredit()
    {
        creditsPanel.gameObject.SetActive(false);

    }

    public void clickAbout()
    {
        aboutPanel.gameObject.SetActive(true);
    }

    public void closeAbout()
    {
        aboutPanel.gameObject.SetActive(false);
    }
}
