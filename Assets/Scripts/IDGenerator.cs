using UnityEngine;
using System;

public class IDGenerator 
{
    private static string ID = "";
    public static bool isGame = false;
    public static string TakeNewID()
    {
        return GenerateID();
    }
    public static string TakeID()
    {
        return ID;
    }

    private static string GenerateID()
    {
        if (!isGame) return null;
        var result = Guid.NewGuid().ToString();
        ID = result;
        Debug.Log(result);
        return result;
    }


}
