using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;


public class ShowScoreboard : MonoBehaviour
{
    Scoreboard[] resultScoreboard = null;

    // Start is called before the first frame update
    public async void Start()
    {
        resultScoreboard = await new GetScoreboard().getTable();
        Debug.Log(resultScoreboard[1].name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
