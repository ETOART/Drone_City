using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    [SerializeField] private S_00_VideoScreen_GameManager _VideoScreen_GameManager;
    public bool isGame = false;

    public static SessionController Instance;
    public static bool gameIsRegister = false;
    private void Awake()
    {
        if (SessionController.Instance != null && SessionController.Instance != this)
        {
            // Уничтожаем текущий объект, если уже есть экземпляр
            Destroy(this.gameObject);
        }
        else
        {
            // Если это первый экземпляр, делаем его постоянным
            SessionController.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public string StartGame()
    {
        if (isGame)
        {
            Debug.Log("Game is already start");
            return null;
        }
        _VideoScreen_GameManager.start = true;
        string ID = IDGenerator.TakeNewID();
        Debug.Log("Generate new ID: " + ID);
        return ID;
    }
}
