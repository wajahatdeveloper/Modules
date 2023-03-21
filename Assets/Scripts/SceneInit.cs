using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInit : MonoBehaviour
{
    #region Singleton

    public static SceneInit Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
}