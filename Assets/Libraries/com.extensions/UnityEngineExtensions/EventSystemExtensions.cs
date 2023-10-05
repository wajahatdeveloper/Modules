using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class EventSystemExtensions
{
    #region IsPointerOnUIElement

    public static bool IsPointerOverUIElement(this EventSystem eventSystem, GameObject gameObject)
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults(eventSystem),gameObject);
    }
    
    private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults, GameObject gameObject)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI") && 
                curRaysastResult.gameObject == gameObject)
                return true;
        }
        return false;
    }
    
    private static List<RaycastResult> GetEventSystemRaycastResults(EventSystem eventSystem)
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    #endregion
}