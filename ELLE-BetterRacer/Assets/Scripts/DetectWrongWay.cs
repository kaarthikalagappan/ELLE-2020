using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWrongWay : MonoBehaviour
{
    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;
    public int previousCheckpoint;
    int tempHolder;
    public GameObject uiObject;


    void Awake()
    {
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
    }

    void Start()
    {
        tempHolder = previousCheckpoint;    
    }

    void Update()
    {
        if (tempHolder == 56)
        {
            tempHolder = -1;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "waypoint")
        {
            return;
        }

        //error right now because I have game object with a number and then(reset) to know which has scripts at the end delete and it will go away
        if (tempHolder > int.Parse(other.gameObject.name))
        {
            uiObject.SetActive(true);
            tempHolder--;
        }
        else { tempHolder = int.Parse(other.gameObject.name); uiObject.SetActive(false);
        }





    }
}
