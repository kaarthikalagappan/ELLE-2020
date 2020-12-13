using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{

    public TMPro.TextMeshProUGUI[] score;
    public TMPro.TextMeshProUGUI[] username;
    

    private int[] keepScore = new int[10];
    private string[] keepUsernames = new string[10];
    private int amount;



    IEnumerator highscores()
    {
        WWWForm form = new WWWForm();
        form.AddField("moduleID", GameManager.moduleID);
        form.AddField("platform", "mb");
        UnityWebRequest getScores = UnityWebRequest.Post("https://endlesslearner.com/api/highscores", form);
        yield return getScores.SendWebRequest();
        if (getScores.isNetworkError || getScores.isHttpError)
        {
            Debug.Log(getScores.error);
        }
        if (getScores.responseCode == 200)
        {
            string temp = getScores.downloadHandler.text;
            temp = fixJson(temp);
            HighList[] High = JsonHelper.FromJson<HighList>(temp);
            amount = High.Length;
            int set = 0;
            Debug.Log(High.Length);
            for (int i = High.Length - 1; i >= 0; i--)
            {
                if (set == 9)
                {
                    break;
                }
                keepScore[set] = High[i].score;
                keepUsernames[set] = High[i].usernames;
                set++;

            }

        }
        ParseTerms();


    }


    [Serializable]
    public class HighList
    {

        public int score;
        public string usernames;

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

    //eventually pass in info once backend is working, should work something like this framework below
    private void ParseTerms()
    {
        for (int i = 0; i < amount; i++)
        {

            username[i].text = keepUsernames[i];
            score[i].text = keepScore[i].ToString();
        }
    }

    int Min(int a, int b)
    {
        if (a > b)
            return b;
        else
            return a;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("highscores");

    }

    // Update is called once per frame
    void Update()
    {
    }
}
