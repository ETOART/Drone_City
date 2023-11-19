using System;
using System.Text;
using System.Net;
using System.Threading;
using UnityEngine;

public class HTTPServer
{
    private HttpListener listener;
    private Thread listenerThread;
    public System.Func<string, string> callback;

    public void Start()
    {
        //callback = str => "";
        listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:8080/");
        listener.Start();

        listenerThread = new Thread(new ThreadStart(Listen));
        listenerThread.Start();

        Debug.Log("Http Server is running at http://localhost:8080/");
    }

    public void OnDestroy()
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
                    var responseString = sessionID;
                    var buffer = Encoding.UTF8.GetBytes(responseString);
                    var response = context.Response;
                    response.ContentLength64 = buffer.Length;
                    var responseOutput = response.OutputStream;
                    responseOutput.Write(buffer, 0, buffer.Length);
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
