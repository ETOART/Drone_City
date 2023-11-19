using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class Scoreboard 
{
    public string name;
    public int score;
    /*

    public async Task<Scoreboard[]> getTable(){
    UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/scoreboard");
    await www.SendWebRequest();
    Scoreboard[] scores = null;

    if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else { 
            Debug.Log(www.downloadHandler.text);
            string jsonScore = "{ \"scoreboards\": " + www.downloadHandler.text + "}" ;
            RootObject root = JsonUtility.FromJson<RootObject>(jsonScore);
            scores = root.scoreboards;
            Debug.Log(scores[0].name); 
        }
    return scores;
}
*/

}  

[System.Serializable]
public class RootObject
{
    public  Scoreboard[] scoreboards;
}

