using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string nextSceneName;

    public void NextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }


}
