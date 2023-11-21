using System;
using System.Text;
using System.Net;
using System.Threading;
using UnityEngine;

public class HTTPServer
{
    private HttpListener listener;
    private Thread listenerThread;
    public System.Func<string,string> callback;

    public void Start()
    {
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();

        listenerThread = new Thread(new ThreadStart(Listen));
        listenerThread.Start();

        Debug.Log("Http Server is running at http://localhost:8080/");
    }

    private void OnDestroy()
    {
        listener.Stop();
        listenerThread.Abort();
    }

    private void Listen()
    {
        while (listener.IsListening)
        {
            var context = listener.GetContext(); 
            ThreadPool.QueueUserWorkItem((_) =>
            {
                try
                {
                    var sessionID = callback("");
                    //var sessionID = "";
                    var responseString = sessionID;
                    var response = context.Response;
                    if (sessionID == null){
                        response.StatusCode = 409;
                        responseString = "CONFLICT";
                        Debug.Log("конфликт, сессия уже запущена");
                    }
                    var buffer = Encoding.UTF8.GetBytes(responseString);
                    response.ContentLength64 = buffer.Length;
                    var responseOutput = response.OutputStream;
                    responseOutput.Write( buffer, 0, buffer.Length);
                    responseOutput.Close();
                    Debug.Log("вернули результат");
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }, context);
        }
    }
}
