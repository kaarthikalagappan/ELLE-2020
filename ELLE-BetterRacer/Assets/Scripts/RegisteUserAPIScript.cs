using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RegisteUserAPIScript : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    public GameObject confirmPassword;
    public GameObject groupID;
    public GameObject groupPassword;
    public string nextSceneName;

    private void Start()
    {
        
    }
    public void register()
    {
        string user = username.GetComponentInChildren<TextMeshProUGUI>().text;
        user = System.Text.RegularExpressions.Regex.Replace(user, @"\t|\n|\r|\u200b", "");
        string pass = password.GetComponentInChildren<TextMeshProUGUI>().text;
        pass = System.Text.RegularExpressions.Regex.Replace(pass, @"\t|\n|\r|\u200b", "");
        string confirmPass = confirmPassword.GetComponentInChildren<TextMeshProUGUI>().text;
        confirmPass = System.Text.RegularExpressions.Regex.Replace(confirmPass, @"\t|\n|\r|\u200b", "");
        string grID = groupID.GetComponentInChildren<TextMeshProUGUI>().text;
        grID = System.Text.RegularExpressions.Regex.Replace(grID, @"\t|\n|\r|\u200b", "");
        string groupPass = groupPassword.GetComponentInChildren<TextMeshProUGUI>().text;
        groupPass = System.Text.RegularExpressions.Regex.Replace(groupPass, @"\t|\n|\r|\u200b", "");

        if (user.Length == 0 || pass.Length == 0 || confirmPass.Length == 0 || grID.Length == 0 || groupPass.Length == 0)
        {
            Debug.Log("Not All Fields are Satisfied");
        }
        else
        {

            if (passwordMatch(pass, confirmPass))
            {
                StartCoroutine(Upload(user, pass, confirmPass, int.Parse(grID), groupPass));

            }
            else
            {
                Debug.Log("Enter Proper passcode");
            }

        }
    }


    bool passwordMatch(string one, string two)
    {
        if (one.Equals(two))
        {
            return true;
        }
        return false;
    }

    IEnumerator Upload(string username, string password, string password_confirm, int groupID, string groupPassword)
    {
        WWWForm form = new WWWForm();
        //form.AddField("username", "mysql");
        //form.AddField("password", "hello");
        //form.AddField("password_confirm", "hello");
        form.AddField("username", username);
        form.AddField("password", password);
        form.AddField("password_confirm", password_confirm);
        form.AddField("groupID", groupID);
        form.AddField("groupPassword", groupPassword);
        UnityWebRequest www = UnityWebRequest.Post("https://endlesslearner.com/api/register", form);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
        }
        if (www.responseCode == 201)
        {
            SceneManager.LoadScene(nextSceneName);
        }

    }
}
