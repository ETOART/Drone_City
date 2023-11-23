using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerStart : MonoBehaviour
{
    [SerializeField] private SessionController sessionController;

    private static ServerStart instance;
    HTTPServer server;
    // Публичное свойство для доступа к единственному экземпляру класса
    public static ServerStart Instance;



    void Awake()
    {

        //if (ServerStart.instance != null && ServerStart.instance != this)
        //{
        //    // Уничтожаем текущий объект, если уже есть экземпляр
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    // Если это первый экземпляр, делаем его постоянным
        //    ServerStart.instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}

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
