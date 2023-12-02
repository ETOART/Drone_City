using UnityEngine;
using System;

public class IDGenerator 
{
    private static string ID = "";
    public static string TakeNewID()
    {
        return GenerateID();
    }
    public static string TakeID()
    {
        if(ID == "")
        return GenerateID();

        return ID;
    }

    private static string GenerateID()
    {
        var result = Guid.NewGuid().ToString();

        ID = result;
        Debug.Log(result);
        return result;
    }


}
