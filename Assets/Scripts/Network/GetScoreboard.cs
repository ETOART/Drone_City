using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
/*
 
public class GetScoreboard : MonoBehaviour {

    public void Start() {
        StartCoroutine(GetText(scoreboard => Debug.Log("Result = " + scoreboard[0].name) ));
    }
 
    IEnumerator GetText(System.Action<Scoreboard[]> callback) {
        UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:8000/scoreboard");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else { 
            Debug.Log(www.downloadHandler.text);
            string jsonScore = "{ \"scoreboards\": " + www.downloadHandler.text + "}" ;
            RootObject root = JsonUtility.FromJson<RootObject>(jsonScore);
            Scoreboard[] scores = root.scoreboards;
            Debug.Log(scores[0].name);
            if (callback != null){
                callback(scores);
            } 
            yield return scores;
        }

    }
}
*/
public class GetScoreboard{

public async UniTask<Scoreboard[]> getTable(){
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
        }
    return scores;
}
}