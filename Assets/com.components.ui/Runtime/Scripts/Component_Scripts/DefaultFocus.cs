using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DefaultFocus : MonoBehaviour
{
    public Selectable controlToFocus;
    public float startDelay = 1.0f;
    
    private void OnEnable()
    {
        StartCoroutine(FocusControl());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator FocusControl()
    {
        yield return new WaitForSeconds(startDelay);
        controlToFocus.Select();
    }
}
