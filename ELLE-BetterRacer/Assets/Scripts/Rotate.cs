using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {   
        if (GameManager.enableSpin)
            transform.Rotate(new Vector3(0f, 1f, 0f));
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void enableRotation()
    {
        GameManager.enableSpin = true;
    }

    public void disableRotation()
    {
        Debug.Log("off");
        GameManager.enableSpin = false;
    }
}
