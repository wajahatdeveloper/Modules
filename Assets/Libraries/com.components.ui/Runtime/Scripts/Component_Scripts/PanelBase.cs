using System;
using UnityEngine;

/// <summary>
/// A component to identify and interfere with Panel gameObjects.
/// </summary>
public class PanelBase : MonoBehaviour
{
    public event Action OnBeforeShown;
    public event Action OnAfterShown;

    public event Action OnBeforeHidden;
    public event Action OnAfterHidden;

    /// <summary>
    /// Activates the panel <see cref="GameObject"/>.
    /// </summary>
    public void Show()
    {
        OnBeforeShow();
        gameObject.SetActive(true);
        OnAfterShow();
    }

    /// <summary>
    /// Deactivates the panel <see cref="GameObject"/>.
    /// </summary>
    public void Hide()
    {
        OnBeforeHide();
        gameObject.SetActive(false);
        OnAfterHide();
    }

    /// <summary>
    /// Called just before showing panel.
    /// </summary>
    protected virtual void OnBeforeShow()
    {
        OnBeforeShown?.Invoke();
    }

    /// <summary>
    /// Called just after showing panel.
    /// </summary>
    protected virtual void OnAfterShow()
    {
        DebugX.Log($"{gameObject.name} : Shown",LogFilters.None, null);
        OnAfterShown?.Invoke();
    }

    /// <summary>
    /// Called just before hiding panel.
    /// </summary>
    protected virtual void OnBeforeHide()
    {
        OnBeforeHidden?.Invoke();
    }

    /// <summary>
    /// Called just after hiding panel.
    /// </summary>
    protected virtual void OnAfterHide()
    {
        OnAfterHidden?.Invoke();
    }
}