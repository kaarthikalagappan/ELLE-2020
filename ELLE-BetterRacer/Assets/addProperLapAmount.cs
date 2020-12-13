using UnityEngine;
using UnityEngine.UI;

public class addProperLapAmount : MonoBehaviour
{
    public Text lapamountText;
    // Start is called before the first frame update
    void Start()
    {
        string lap = PlayerPrefs.GetString("lapAmount");
        lapamountText.text = "LAP: 1/" + lap;
    }


}
