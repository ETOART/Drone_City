using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    [SerializeField] private S_00_VideoScreen_GameManager _VideoScreen_GameManager;
    public bool isGame = false;

    private void Awake()
    {
        DontDestroyOnLoad(transform);
    }
    public string StartGame()
    {
        if (isGame)
        {
            Debug.Log("Game is already start");
            return null;
        }
        _VideoScreen_GameManager.StartGame();
        string ID = IDGenerator.TakeNewID();
        Debug.Log("Generate new ID: " + ID);
        return ID;
    }
}
