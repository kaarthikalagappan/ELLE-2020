using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Runtime.Serialization.Json;
using System;

public class DeckSelector : MonoBehaviour
{
    public GameObject DeckText;
    Text questionText;
    public GameObject MovingText;
    Text moveText;
    string [] moduleNames;
    int [] moduleIDs;

    public GameObject lapAmount;
    public GameObject spinTime;
    public GameObject speed;


    int currentPosition;
    int maxPosition;



    // Start is called before the first frame update
    void Start()
    {
        questionText = DeckText.GetComponent<Text>();
        moveText = MovingText.GetComponent<Text>();

        WWWForm form = new WWWForm();
        StartCoroutine(PopulateMods());
        currentPosition = 0;
        moduleNames = new string[30];
        moduleIDs = new int[30];
    }



    IEnumerator PopulateMods()
    {
        UnityWebRequest getModules = UnityWebRequest.Get("https://endlesslearner.com/api/modules");
        getModules.SetRequestHeader("Authorization", "Bearer " + GameManager.token);
        yield return getModules.SendWebRequest();
        if (getModules.isNetworkError || getModules.isHttpError)
        {
            Debug.Log(getModules.error);
        }
        if (getModules.responseCode == 200)
        {
            string temp = getModules.downloadHandler.text;
            temp = fixJson(temp);
            Modules[] modules = JsonHelper.FromJson<Modules>(temp);
            for(int i=0; i < modules.Length; i++)
            {
                moduleNames[i] = modules[i].name;
                moduleIDs[i] = modules[i].moduleID;

            }
            maxPosition = modules.Length;
        }

    }

    [Serializable]
    public class Modules
    {
        public int moduleID;
        public int groupID;
        public string name;
        public string language;
        public string complexity;

    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    string getCurrentDeck()
    {
        return moduleNames[currentPosition];
    }

    public void left()
    {
        if (currentPosition == 0)
        {
            return;
        }
        else
        {
            currentPosition--;
        }
    }

    public void right()
    {
        if (currentPosition == maxPosition - 1)
        {
            return;
        }
        else
        {
            currentPosition++;
        }
    }

    public void enter()
    {
        string lap = lapAmount.GetComponentInChildren<TextMeshProUGUI>().text;
        lap = System.Text.RegularExpressions.Regex.Replace(lap, @"\t|\n|\r|\u200b", "");
        if(lap.Length >= 1)
        {
            PlayerPrefs.SetString("lapAmount", lap);
        }
        string spin = spinTime.GetComponentInChildren<TextMeshProUGUI>().text;
        spin = System.Text.RegularExpressions.Regex.Replace(spin, @"\t|\n|\r|\u200b", "");
        if(spin.Length >= 0)
        {
            PlayerPrefs.SetString("burnoutsecs", spin);
        }
        string speedAmount = speed.GetComponentInChildren<TextMeshProUGUI>().text;
        speedAmount = System.Text.RegularExpressions.Regex.Replace(speedAmount, @"\t|\n|\r|\u200b", "");
        if (speedAmount.Length >= 1)
        {
            PlayerPrefs.SetString("speedAmount", speedAmount);
        }

        questionText.text = moduleNames[currentPosition];
        GameManager.moduleID = moduleIDs[currentPosition].ToString();
        GameManager.moduleName = moduleNames[currentPosition];

    }



    void Update()
    {
        moveText.text = getCurrentDeck();
    }
}
