/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;


public class SendScore(){

    private int here_score;
    private string here_session_id;
    private string url;




    public async UniTask send(url url, here_score here_score, here_session_id here_session_id){


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
        
        await request.SendWebRequest();

        
    } 


}
*/