#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class UIDefaultState : MonoBehaviour
{
    public List<GameObject> enabledByDefault = new();
    [Space]
    public List<GameObject> disabledByDefault = new();

    private void OnEnable()
    {
        EditorSceneManager.sceneClosing += Handle_EditorOnSceneClosing;
        EditorSceneManager.activeSceneChangedInEditMode += Handle_OnSceneChangedInEditMode;
    }

    private void Handle_OnSceneChangedInEditMode(Scene arg0, Scene arg1)
    {
    }

    private void OnDisable()
    {
        EditorSceneManager.sceneClosing -= Handle_EditorOnSceneClosing;
    }

    private void Handle_EditorOnSceneClosing(Scene scene, bool removingscene)
    {
        Apply();
    }

    [Button]
    private void Apply()
    {
        if (Application.isPlaying) { return; }

        bool isDirty = false;
        List<string> enabledObjects = new();
        List<string> disabledObjects = new();

        foreach (GameObject o in enabledByDefault)
        {
            if (!o.activeSelf)
            {
                o.SetActive(true);
                enabledObjects.Add(o.name);
                isDirty = true;
            }
        }

        foreach (GameObject o in disabledByDefault)
        {
            if (o.activeSelf)
            {
                o.SetActive(false);
                disabledObjects.Add(o.name);
                isDirty = true;
            }
        }

        if (isDirty)
        {
            EditorSceneManager.MarkAllScenesDirty();

            Debug.Log($"UIDefaultState : UI Default State Updated.");

            if (enabledObjects.IsNotEmpty())
            {
                Debug.Log($"UIDefaultState : Enabled Objects: \n{enabledObjects.StringJoin("\n")}.");
            }

            if (disabledObjects.IsNotEmpty())
            {
                Debug.Log($"UIDefaultState : Disabled Objects: \n{disabledObjects.StringJoin("\n")}.");
            }

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
    }
}
#endif