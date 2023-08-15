using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerX
{
    private static string LogClassName = "SceneManagerX";

    public static void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex + 2 > SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Scene Manager: No Next Scene Available to Load");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    public static void LoadScene(int index)
    {
        DebugX.Log($"{LogClassName} : Loading Scene {index}.",Color.magenta, "", null);
        SceneManager.LoadScene(index);
    }
    
    public static void LoadPreviousScene()
    {
        if (SceneManager.GetActiveScene().buildIndex - 1 < 0)
        {
            Debug.LogWarning("Scene Manager: No Previous Scene Available to Load");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public static void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}