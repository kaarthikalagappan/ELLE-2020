using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCarChange : MonoBehaviour
{
    public Material sportsCarFrame;
    public Material grayPaint;
    public Material yellowPaint;
    public Material redPaint;
    public Material bluePaint;
    public Material greenPaint;
    public Material orangePaint;
    public GameObject[] carImages;
    public int currentPosition = 0;
    // Start is called before the first frame update
    void Start()
    {
        //sportsCarFrame.color = Color.grey;
    }

    public void left()
    {
        if (currentPosition == 0)
        {
            return;
        }
        else
        {
            carImages[currentPosition].SetActive(false);
            currentPosition--;
        }
    }

    public void right()
    {
        if (currentPosition == carImages.Length - 1)
        {
            Debug.Log("Filled");
            return;
        }
        else
        {
            carImages[currentPosition].SetActive(false);
            currentPosition++;
        }
        Debug.Log(currentPosition);
    }

    public void enter()
    {
        PlayerPrefs.SetInt("Color", currentPosition);
    }

    void Update()
    {
        carImages[currentPosition].SetActive(true);
    }
}

