using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugX
{
    public static void Log(string inLog,string inFilterName, GameObject inContext)
    {
        #if CONSOLE_PRO
        ConsoleProDebug.LogToFilter(inLog,inFilterName,inContext);
        #else
        Debug.Log(inFilterName + " : " + inLog, inContext);
        #endif
    }
}