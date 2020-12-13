using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject panel;
    public Joystick Joystick;
    public Text deckName;
    //public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameManager.moduleName);
        deckName.text = GameManager.moduleName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause()
    {
        panel.gameObject.SetActive(true);
        Joystick.gameObject.SetActive(false);
        Time.timeScale = 0;
        //button.gameObject.SetActive(true);

    }

    public void unpause()
    {
        panel.gameObject.SetActive(false);
        Joystick.gameObject.SetActive(true);
        Time.timeScale = 1;
        //button.gameObject.SetActive(true);

    }


}
