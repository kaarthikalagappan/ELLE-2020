using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System;

public class QuizTrigger : MonoBehaviour
{

    public GameObject question;
    public Text questionText;

    public GameObject answer;
    public Text answerText;

    public GameObject itemBoxes;
    public GameObject itemRow;

    public GameObject previousItemRow;

    public GameObject[] images = new GameObject[3];

    private Sprite targetSprite;
    private HashSet<int> numberSeen = new HashSet<int>();
    private HashSet<int> imageSeen = new HashSet<int>();


    List<Pair> pairs = new List<Pair>();
    List<Pair> pairsImage = new List<Pair>();
    List<Pair> questionIDToTermID = new List<Pair>();
    List<Pair> questionIDToAnswer = new List<Pair>();



    void Start()
    {
        StartCoroutine(PopulateQuestionsAnswers());
        Time.timeScale = 1;
    }

    IEnumerator PopulateQuestionsAnswers()
    {
        WWWForm form = new WWWForm();
        form.AddField("moduleID", GameManager.moduleID);
        Debug.Log("flak: " + GameManager.moduleID);
        UnityWebRequest getQuesAns = UnityWebRequest.Post("https://endlesslearner.com/api/modulequestions", form);
        getQuesAns.SetRequestHeader("Authorization", "Bearer " + GameManager.token);
        yield return getQuesAns.SendWebRequest();
        if (getQuesAns.isNetworkError || getQuesAns.isHttpError)
        {
            Debug.Log(getQuesAns.error);
        }
        if (getQuesAns.responseCode == 200)
        {
            string temp = getQuesAns.downloadHandler.text;
            Debug.Log(temp);
            temp = fixJson(temp);
            ModulesQues[] modulesques = JsonHelper.FromJson<ModulesQues>(temp);
            for (int i = 0; i < modulesques.Length; i++)
            {


                //Example pair(Question: "What is the translation of bonjour", Answer: either "hello" or exampleimage.png)
                for(int j = 0; j < modulesques[i].answers.Count; j++)
                {
                    //This makes a question for each module to question with the img being the one on the term. So it gets as many images as possible
                    //pairs.Add(new Pair(modulesques[i].questionText + ' ' + modulesques[i].answers[j].front, modulesques[i].answers[j].imageLocation));

                    //This makes a question and answer pair but all answers share the img on the question HOW I THINK IT WILL BE IF NOT USE ABOVE\

                    //If backend changes then just need to call questionText since it'll automatically be linked with the front of the questopm
                    //pairs.Add(new Pair(modulesques[i].questionText, modulesques[i].imageLocation));

                    if (modulesques[i].type.Equals("IMAGE"))
                    {
                        pairs.Add(new Pair(modulesques[i].questionText + ' ' + modulesques[i].answers[j].front, modulesques[i].imageLocation));
                        questionIDToTermID.Add(new Pair(modulesques[i].questionID, modulesques[i].answers[j].termID));
                        questionIDToAnswer.Add(new Pair(modulesques[i].questionID, modulesques[i].answers[j].back));

                    }
                    if (modulesques[i].type.Equals("LONGFORM") && (modulesques[i].imageLocation.Contains(".jpg") || modulesques[i].imageLocation.Contains(".png")))
                    {
                        pairs.Add(new Pair(modulesques[i].questionText + ' ' + modulesques[i].answers[j].front, modulesques[i].imageLocation));
                        questionIDToTermID.Add(new Pair(modulesques[i].questionID, modulesques[i].answers[j].termID));
                        questionIDToAnswer.Add(new Pair(modulesques[i].questionID, modulesques[i].answers[j].back));

                    }

                    if (modulesques[i].type.Equals("MATCH") && (modulesques[i].answers[j].imageLocation.Contains(".jpg") || modulesques[i].answers[j].imageLocation.Contains(".png") || modulesques[i].answers[j].imageLocation.Contains(".PNG")))
                    {
                        pairs.Add(new Pair(modulesques[i].questionText, modulesques[i].answers[j].imageLocation));
                        questionIDToTermID.Add(new Pair(modulesques[i].questionID, modulesques[i].answers[j].termID));
                        questionIDToAnswer.Add(new Pair(modulesques[i].questionID, modulesques[i].answers[j].back));

                    }
                    //Bottom one can add questions with audio? Maybe find a library that can play the audio dont know how it works.
                    //pairs.Add(new Pair(modulesques[i].questionText + ' ' + modulesques[i].answers[j].front, modulesques[i].audioLocation));
                }
                pairsImage.Add(new Pair(modulesques[i].questionID.ToString(), modulesques[i].imageLocation));
            }

        }

    }

    [Serializable]
    public class Answer
    {
        public string termID;
        public string audioLocation;
        public string imageLocation;
        public string front;
        public string back;
        public string type;
        public string gender;
        public string language;

    }

    [Serializable]
    public class ModulesQues
    {
        public string questionID;
        public string audioLocation;
        public string imageLocation;
        public string type;
        public string questionText;
        public List<Answer> answers = new List<Answer>();
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


    IEnumerator GetTextureRequest(string url, System.Action<Sprite> callback)
    {
        using (var www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {

                    var texture = DownloadHandlerTexture.GetContent(www);
                    var rect = new Rect(0, 0, texture.width, texture.height);
                    var sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 100f);
                    callback(sprite);
                }
            }
        }
    }

    void getOnSprite(int index, string url)
    {
        StartCoroutine(GetTextureRequest(url, (response) =>
        {
            targetSprite = response;
            images[index].GetComponent<SpriteRenderer>().sprite = targetSprite;
            images[index].GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
            images[index].GetComponent<SpriteRenderer>().size = new Vector2(10, 10);
        }));

        string name = pairs[shuffleImage()].getAnswer();
        string otherurl = "https://endlesslearner.com/" + name;

        int randomIntForPic = shuffle();
        images[randomIntForPic].name = name;
        StartCoroutine(GetTextureRequest(otherurl, (response) =>
        {
            targetSprite = response;
            images[randomIntForPic].GetComponent<SpriteRenderer>().sprite = targetSprite;
            images[randomIntForPic].GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
            images[randomIntForPic].GetComponent<SpriteRenderer>().size = new Vector2(10, 10);

        }));
        name = pairs[shuffleImage()].getAnswer();
        otherurl = "https://endlesslearner.com/" + name;
        int randomIntForPic2 = shuffle();
        images[randomIntForPic2].name = name;
        StartCoroutine(GetTextureRequest(otherurl, (response) =>
        {
            targetSprite = response;
            images[randomIntForPic2].GetComponent<SpriteRenderer>().sprite = targetSprite;
            images[randomIntForPic2].GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
            images[randomIntForPic2].GetComponent<SpriteRenderer>().size = new Vector2(10, 10);
        }));
        numberSeen.Clear();
        imageSeen.Clear();
       
    }

    int shuffle()
    {
        int index = UnityEngine.Random.Range(0, 3);
        while (numberSeen.Contains(index))
        {
            index = UnityEngine.Random.Range(0, 3);
        }
        numberSeen.Add(index);
        return index;
    }

    int shuffleImage()
    {
        int index = (UnityEngine.Random.Range(0, int.MaxValue) % pairs.Count);
        while (imageSeen.Contains(index))
        {
            index = (UnityEngine.Random.Range(0, int.MaxValue) % pairs.Count);
        }
        imageSeen.Add(index);
        return index;
    }



    void OnTriggerEnter(Collider other)
    {
        int index = shuffleImage();

        question = GameObject.Find("/Canvas/Question");
        questionText = question.GetComponent<Text>();
        questionText.text = pairs[index].getQuestion();
        questionText.text = questionText.text;
        GameManager.questionID = questionIDToTermID[index].getQuestion();
        GameManager.termID = questionIDToTermID[index].getAnswer();
        GameManager.correctAnswerToShow = questionIDToAnswer[index].getAnswer();

        answer = GameObject.Find("/Canvas/Answer");
        answerText = answer.GetComponent<Text>();
        answerText.text = pairs[index].getAnswer();
        int randomIntForPic = shuffle();
        images[randomIntForPic].name = answerText.text;
        string correctPicURL = "https://endlesslearner.com/" + answerText.text;
        getOnSprite(randomIntForPic, correctPicURL);



        itemRow.transform.GetChild(0).gameObject.SetActive(true);
        itemRow.transform.GetChild(1).gameObject.SetActive(true);
        itemRow.transform.GetChild(2).gameObject.SetActive(true);

        previousItemRow.transform.GetChild(0).gameObject.SetActive(false);
        previousItemRow.transform.GetChild(1).gameObject.SetActive(false);
        previousItemRow.transform.GetChild(2).gameObject.SetActive(false);

        CarBehaviour.currentItemRow = itemRow;
    }
}
