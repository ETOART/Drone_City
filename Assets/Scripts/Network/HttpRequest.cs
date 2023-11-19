using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;


public class HttpRequest : MonoBehaviour
{
    [SerializeField] private string url;
    [SerializeField] private int here_score;

    [SerializeField] private string here_session_id;

    public void Start()
    {
        StartCoroutine(SendRequest());
    }

    void Update()
    {
        
    }
    

    private IEnumerator SendRequest(){

        WWWForm scoreData = new WWWForm();

        ScoreStruc score = new ScoreStruc()
        {

            session_id = here_session_id,

            score = here_score

        };
        
        string json = JsonUtility.ToJson(score);

        UnityWebRequest request = UnityWebRequest.Post(this.url, scoreData);

        byte[] scoreBytes = Encoding.UTF8.GetBytes(json);

        UploadHandler uploadHandler = new UploadHandlerRaw(scoreBytes);

        request.uploadHandler = uploadHandler;

        //request.SendRequestHeader();
        
        yield return request.SendWebRequest();



    }



}
