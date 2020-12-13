using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LogOutAPI : MonoBehaviour
{
    public void click()
    {
        StartCoroutine(LogOut());
    }

    IEnumerator LogOut()
    {
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Post("https://endlesslearner.com/api/logout", form);
        www.SetRequestHeader("Authorization", "Bearer " + GameManager.token);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        if (www.responseCode == 200)
        {
            SceneManager.LoadScene("Account Menu");
        }

    }
}
