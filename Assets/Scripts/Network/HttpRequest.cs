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

    public void SendScore( int _score)
    {
        StartCoroutine(SendRequest( _score));
    }

    private IEnumerator SendRequest(int _score)
    {

        WWWForm scoreData = new WWWForm();

        ScoreStruc score = new ScoreStruc()
        {

            session_id = IDGenerator.TakeID(),

            score = _score

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
