using UnityEngine;

[ExecuteInEditMode]
/// <summary>
/// Keeps the child gameObject scaling fixed on scaling the parent.
/// </summary>
public class ScaleParentButNotChild : MonoBehaviour
{
    public bool compensateYPosition = true;
    [SerializeField] GameObject _parentObject;
    [SerializeField] GameObject _childObject;
    Vector3 _initialParentScale;
    Vector3 _initialChildScale;
    Vector3 _initialChildPositionOffset;

    private void Start()
    {
        _initialParentScale = _parentObject.transform.localScale;
        _initialChildScale = _childObject.transform.localScale;
        _initialChildPositionOffset = _childObject.transform.position;
    }

    private void Update()
    {
        ScaleParentObjectButNotChild(_parentObject, _childObject, _initialChildScale);
    }

    void ScaleParentObjectButNotChild(GameObject parentObject, GameObject childObject, Vector3 initialChildScale)
    {
        /*if (!IsValidParentChild(parentObject, childObject))
            return;*/
        var parentScale = parentObject.transform.localScale.x;
        var newChildScale = 1.0f / parentScale;
        childObject.transform.localScale = Vector3.one * newChildScale;

        // Compensate for the change in position
        if (compensateYPosition)
        {
            childObject.transform.position = childObject.transform.position.SetY(_initialChildPositionOffset.y/* + newChildScale*/);
        }
    }
    /*bool IsValidParentChild(GameObject parentObject, GameObject childObject)
    {
        if (childObject.transform.parent == parentObject.transform)
            return true;
        Debug.LogError("Objects are not in Parent-Child relationship");
        return false;
    }*/
}