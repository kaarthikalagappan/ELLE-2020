using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class LogInAPI : MonoBehaviour
{

    public GameObject username;
    public GameObject password;
    public string nextSceneName;
    public string OldToken;

    public void LogIn()
    {
        string user = username.GetComponentInChildren<TextMeshProUGUI>().text;
        user = System.Text.RegularExpressions.Regex.Replace(user, @"\t|\n|\r|\u200b", "");
        string pass = password.GetComponentInChildren<TextMeshProUGUI>().text;
        pass = System.Text.RegularExpressions.Regex.Replace(pass, @"\t|\n|\r|\u200b", "");
        StartCoroutine(LoggingIn(user, pass));
    }

    private void Start()
    {
        string c = PlayerPrefs.GetString("token");
        if (c.Length > 4) {
            StartCoroutine(checkIfLoggedIn(c));
        }
    }

    IEnumerator checkIfLoggedIn(string token)
    {
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        UnityWebRequest www = UnityWebRequest.Post("https://endlesslearner.com/api/activejwt", form);
        www.SetRequestHeader("Authorization", "Bearer " + token);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        if(www.responseCode == 200)
        {
            GameManager.userID = www.downloadHandler.text;
            GameManager.token = token;
            SceneManager.LoadScene(nextSceneName);
        }
    }

    IEnumerator LoggingIn(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        UnityWebRequest www = UnityWebRequest.Post("https://endlesslearner.com/api/login", form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        if (www.responseCode == 200)
        {
            string str = www.downloadHandler.text;
            Debug.Log(str);
            str = str.Replace('{', '\t');
            str = str.Replace('}', '\t');
            str = System.Text.RegularExpressions.Regex.Replace(str, @"\t|\n|\r|\u200b", "");
            string[] arr = str.Split(',');
            GameManager.token = arr[0].Substring(17);
            GameManager.token = GameManager.token.Replace('"', ' ').Trim();
            Debug.Log(GameManager.token);
            GameManager.userID = arr[1].Substring(6);
            PlayerPrefs.SetString("token", GameManager.token);
            SceneManager.LoadScene(nextSceneName);
        }

    }
}
