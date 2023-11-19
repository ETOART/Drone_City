using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerStart : MonoBehaviour
{

    [SerializeField] private S_00_VideoScreen_GameManager _VideoScreen_GameManager;
    // Start is called before the first frame update
    void Start()
    {
        var server = new HTTPServer();
        // ниже передать функцию начала игровой сессии, котоаря возвращает строку с ssesionID
        server.callback = str => _VideoScreen_GameManager.TakeNewID();
        server.Start();

        Debug.Log("Стартанул");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
