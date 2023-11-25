using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class S_04_Final_GameManager : MonoBehaviour
{


    [SerializeField] private ScoreList_group_Script _scoreListGroupScript;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject scoreListBody;
    [SerializeField] private ShowScoreboard showScoreboard;

    


    Scoreboard[] resultScoreboard = null;

    void  Start()
    {
        
        TakeScore();

    }
    private async void TakeScore()
    {
        resultScoreboard = await new GetScoreboard().getTable();

        for (int i = 0; i < resultScoreboard.Length; i++)
        {
            GameObject o = Instantiate(scoreListBody, content.transform);
            ScoreList_group_Script _scoreListGroupScriptO = o.transform.GetChild(0).GetComponent<ScoreList_group_Script>();
            _scoreListGroupScriptO.setName(resultScoreboard[i].name);
            _scoreListGroupScriptO.setScore(resultScoreboard[i].score);
        }
    }
    


}
