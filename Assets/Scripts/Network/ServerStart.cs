using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerStart : MonoBehaviour
{

    [SerializeField] private SessionController sessionController;
    // Start is called before the first frame update
    HTTPServer server;

    public bool isGame = false;
    void Awake()
    {
        DontDestroyOnLoad(transform);
        server = new HTTPServer();
        // ниже передать функцию начала игровой сессии, котоаря возвращает строку с ssesionID
        server.callback = str => sessionController.StartGame();
        server.Start();

        Debug.Log("Стартанул");
    }

    private void OnDestroy()
    {
        server.OnDestroy();
    }


}
