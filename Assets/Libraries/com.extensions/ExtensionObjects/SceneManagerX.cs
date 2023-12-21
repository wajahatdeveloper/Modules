using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerX
{
    private const string LogClassName = "SceneManagerX";

    public static void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex + 2 > SceneManager.sceneCountInBuildSettings)
        {
            DebugX.Log($"{LogClassName} : No Next Scene Available to Load.",Color.magenta, "", null);
        }
        else
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
            DebugX.Log($"{LogClassName} : No Previous Scene Available to Load.",Color.magenta, "", null);
        }
        else
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public static void RestartCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}