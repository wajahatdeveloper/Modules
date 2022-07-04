using UnityEngine;

public class RotationControl : MonoBehaviour
{
    public Vector3 offset;
    
    private Vector3 startingRotation;
    
    void Awake()
    {
        startingRotation = transform.eulerAngles;
    }
    void LateUpdate()
    {
        transform.eulerAngles = startingRotation + offset;
    }
}
