using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateText : MonoBehaviour
{
    public GameObject question;
    public float tick = 0;
    public bool goingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        question = GameObject.Find("/Canvas/Question");
    }

    // Update is called once per frame
    void Update()
    {
            if (tick > 100) {
                goingUp = false;
            }
            
            if (tick < 0) {
                goingUp = true;
            }

            if (goingUp) {
                tick++;
                question.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                question.transform.position += new Vector3(-0.8f, 0f, 0f);
            }
            else {
                tick--;
                question.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                question.transform.position -= new Vector3(-0.8f, 0f, 0f);
            }
    }
}
