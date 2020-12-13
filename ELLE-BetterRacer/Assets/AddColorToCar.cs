using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColorToCar : MonoBehaviour
{
    public Material sportsCarFrame;
    public Material grayPaint;
    public Material yellowPaint;
    public Material redPaint;
    public Material bluePaint;
    public Material orangePaint;
    public Material greenPaint;

    public Material returncolorfromint(int index)
    {
        if (index == 0)
        {
            return redPaint;
        }
        if (index == 1)
        {
            return yellowPaint;
        }
        if (index == 2)
        {
            return grayPaint;
        }
        if (index == 3)
        {
            return bluePaint;
        }
        if (index == 4)
        {
            return orangePaint;
        }
        if (index == 5)
        {
            return greenPaint;
        }
        return grayPaint;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Your color int" + PlayerPrefs.GetInt("Color"));
        Material temp = returncolorfromint(PlayerPrefs.GetInt("Color"));
        sportsCarFrame.color = temp.color;
        
    }


}
