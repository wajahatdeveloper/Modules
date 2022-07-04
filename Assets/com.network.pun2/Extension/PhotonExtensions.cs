using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public static class PhotonExtensions
{
    public static bool HasVariable(this Room room, string key)
    {
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(key))
        {
            return false;
        }

        return true;
    }
    
    public static void SetVariable_Int(this Room room,string key, int value)
    {
        if (room == null || room.CustomProperties == null)
        {
            return;
        }

        Hashtable turnProps = new Hashtable();
        turnProps[key] = value;
        room.SetCustomProperties(turnProps);
    }
    
    public static int GetVariable_Int(this Room room,string key)
    {
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(key))
        {
            return 0;
        }

        return (int) room.CustomProperties[key];
    }
    
    public static void SetVariable_String(this Room room,string key, string value)
    {
        if (room == null || room.CustomProperties == null)
        {
            return;
        }

        Hashtable turnProps = new Hashtable();
        turnProps[key] = value;
        room.SetCustomProperties(turnProps);
    }
    
    public static string GetVariable_String(this Room room,string key)
    {
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(key))
        {
            return "";
        }

        return (string) room.CustomProperties[key];
    }
}
