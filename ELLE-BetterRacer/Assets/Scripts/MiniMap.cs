using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private LineRenderer lineRendere;
    private GameObject TrackPath;
    public GameObject LocalPlayer;
    public GameObject MiniMapCam;
    public GameObject SkyViewPlayer;


    Color c1 = Color.blue;
    Color c2 = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        lineRendere = GetComponent<LineRenderer>();
        TrackPath = this.gameObject;
        lineRendere.startColor = Color.green;
        int num_of_path = TrackPath.transform.childCount;
        lineRendere.positionCount = num_of_path + 1;

        for (int i = 0; i < num_of_path; i++)
        {
            lineRendere.SetPosition(i, new Vector3(TrackPath.transform.GetChild(i).transform.position.x, 4,
                TrackPath.transform.GetChild(i).transform.position.z));
        }
        lineRendere.SetPosition(num_of_path, lineRendere.GetPosition(0));
        lineRendere.startWidth = 7f;
        lineRendere.endWidth = 7f;
    }

    // Update is called once per frame
    void Update()
    {

        MiniMapCam.transform.position = (new Vector3(LocalPlayer.transform.position.x,
            MiniMapCam.transform.position.y, LocalPlayer.transform.position.z));


        SkyViewPlayer.transform.position = (new Vector3(LocalPlayer.transform.position.x,
            SkyViewPlayer.transform.position.y, LocalPlayer.transform.position.z));
    }
}
