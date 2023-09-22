using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/* *****************************************************************************
 * File:    UnityRotateExtensions.cs
 * Author:  Philip Pierce - Wednesday, October 29, 2014
 * Description:
 *  Extensions for rotating in Unity
 *  
 * History:
 *  Wednesday, October 29, 2014 - Created
 * ****************************************************************************/

/// <summary>
/// Extensions for rotating in Unity
/// </summary>
public static class UnityRotateExtensions
{
    #region Rotate_DegreesPerSecond

    /// <summary>
    /// Rotates the game object around <paramref name="direction"/> every <paramref name="timeInSeconds"/>.
    /// Using 1 for <paramref name="timeInSeconds"/> will result in the object rotating <paramref name="direction"/> degress in one second.
    /// NOTE: Must be called from Update()
    /// </summary>
    /// <param name="go"></param>
    /// <param name="direction"></param>
    /// <param name="timeInSeconds"></param>
    public static void Rotate_DegreesPerSecond(this GameObject go, Vector3 direction, float timeInSeconds)
    {
        Rotate_DegreesPerSecond(go.transform, direction, timeInSeconds);   
    }

    /// <summary>
    /// Rotates the game object around <paramref name="direction"/> every <paramref name="timeInSeconds"/>.
    /// Using 1 for <paramref name="timeInSeconds"/> will result in the object rotating <paramref name="direction"/> degress in one second.
    /// NOTE: Must be called from Update()
    /// </summary>
    /// <param name="goTrans"></param>
    /// <param name="direction"></param>
    /// <param name="timeInSeconds"></param>
    public static void Rotate_DegreesPerSecond(this Transform goTrans, Vector3 direction, float timeInSeconds)
    {
        goTrans.Rotate(direction * timeInSeconds * Time.deltaTime);
    }

    // Rotate_DegreesPerSecond
    #endregion

    #region RotateAroundAxis

    #region RotateAroundAxis_X

    /// <summary>
    /// Rotates the game object around the X axis, rotating <paramref name="degrees"/> every <paramref name="timeInSeconds"/>.
    /// Using 1 for <paramref name="timeInSeconds"/> will result in the object rotating <paramref name="degrees"/> in one second.
    /// NOTE: Must be called from Update()
    /// </summary>
    /// <param name="go"></param>
    /// <param name="degrees"></param>
    /// <param name="timeInSeconds"></param>
    public static void RotateAroundAxis_X(this GameObject go, float degrees, float timeInSeconds)
    {
        RotateAroundAxis_X(go.transform, degrees, timeInSeconds);
    }

    /// <summary>
    /// Rotates the game object around the X axis, rotating <paramref name="degrees"/> every <paramref name="timeInSeconds"/>.
    /// Using 1 for <paramref name="timeInSeconds"/> will result in the object rotating <paramref name="degrees"/> in one second.
    /// NOTE: Must be called from Update()
    /// </summary>
    /// <param name="goTrans"></param>
    /// <param name="degrees"></param>
    /// <param name="timeInSeconds"></param>
    public static void RotateAroundAxis_X(this Transform goTrans, float degrees, float timeInSeconds)
    {
        Rotate_DegreesPerSecond(goTrans, new Vector3(degrees, 0, 0), timeInSeconds);
    }

    // RotateAroundAxis_X
    #endregion

    #region RotateAroundAxis_Y

    /// <summary>
    /// Rotates the game object around the Y axis, rotating <paramref name="degrees"/> every <paramref name="timeInSeconds"/>.
    /// Using 1 for <paramref name="timeInSeconds"/> will result in the object rotating <paramref name="degrees"/> in one second.
    /// NOTE: Must be called from Update()
    /// </summary>
    /// <param name="go"></param>
    /// <param name="degrees"></param>
    /// <param name="timeInSeconds"></param>
    public static void RotateAroundAxis_Y(this GameObject go, float degrees, float timeInSeconds)
    {
        RotateAroundAxis_Y(go.transform, degrees, timeInSeconds);
    }

    /// <summary>
    /// Rotates the game object around the Y axis, rotating <paramref name="degrees"/> every <paramref name="timeInSeconds"/>.
    /// Using 1 for <paramref name="timeInSeconds"/> will result in the object rotating <paramref name="degrees"/> in one second.
    /// NOTE: Must be called from Update()
    /// </summary>
    /// <param name="goTrans"></param>
    /// <param name="degrees"></param>
    /// <param name="timeInSeconds"></param>
    public static void RotateAroundAxis_Y(this Transform goTrans, float degrees, float timeInSeconds)
    {
        Rotate_DegreesPerSecond(goTrans, new Vector3(0, degrees, 0), timeInSeconds);
    }

    // RotateAroundAxis_Y
    #endregion

    #region RotateAroundAxis_Z

    /// <summary>
    /// Rotates the game object around the Z axis, rotating <paramref name="degrees"/> every <paramref name="timeInSeconds"/>.
    /// Using 1 for <paramref name="timeInSeconds"/> will result in the object rotating <paramref name="degrees"/> in one second.
    /// NOTE: Must be called from Update()
    /// </summary>
    /// <param name="go"></param>
    /// <param name="degrees"></param>
    /// <param name="timeInSeconds"></param>
    public static void RotateAroundAxis_Z(this GameObject go, float degrees, float timeInSeconds)
    {
        RotateAroundAxis_Z(go.transform, degrees, timeInSeconds);
    }

    /// <summary>
    /// Rotates the game object around the Z axis, rotating <paramref name="degrees"/> every <paramref name="timeInSeconds"/>.
    /// Using 1 for <paramref name="timeInSeconds"/> will result in the object rotating <paramref name="degrees"/> in one second.
    /// NOTE: Must be called from Update()
    /// </summary>
    /// <param name="goTrans"></param>
    /// <param name="degrees"></param>
    /// <param name="timeInSeconds"></param>
    public static void RotateAroundAxis_Z(this Transform goTrans, float degrees, float timeInSeconds)
    {
        Rotate_DegreesPerSecond(goTrans, new Vector3(0, 0, degrees), timeInSeconds);
    }

    // RotateAroundAxis_Z
    #endregion

    // RotateAroundAxis
    #endregion

    #region LookAt

    /// <summary>
    /// No matter where the <paramref name="targetGo"/> goes, rotate toward him, like a gun turret following a target.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="targetGo">object to look at</param>
    public static void LookAt(this GameObject go, GameObject targetGo)
    {
        LookAt(go, targetGo.transform);
    }

    /// <summary>
    /// No matter where the <paramref name="targetTrans"/> goes, rotate toward him, like a gun turret following a target.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="targetTrans">object to look at</param>
    public static void LookAt(this GameObject go, Transform targetTrans)
    {
        go.transform.LookAt(targetTrans);
    }

    /// <summary>
    /// No matter where the <paramref name="targetVector"/> goes, rotate toward him, like a gun turret following a target.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="targetVector">object to look at</param>
    public static void LookAt(this GameObject go, Vector3 targetVector)
    {
        go.transform.LookAt(targetVector);
    }

    // LookAt
    #endregion

    /// <summary>
    /// Quaternion raised to a power.
    /// This is useful for smoothly multiplying a Quaternion by a given floating-point value.
    /// </summary>
    public static Quaternion Pow(this Quaternion quaternion, float power)
    {
        float magnitude = quaternion.Magnitude();
        Vector3 normalizedVector = new Vector3(quaternion.x, quaternion.y, quaternion.z).normalized;

        return new Quaternion(normalizedVector.x, normalizedVector.y, normalizedVector.z, 0)
            .ScalarMultiply(power * Mathf.Acos(quaternion.w / magnitude))
            .Exp()
            .ScalarMultiply(Mathf.Pow(magnitude, power));
    }

    /// <summary>
    /// Euler's number raised to quaternion.
    /// </summary>
    public static Quaternion Exp(this Quaternion quaternion)
    {
        Vector3 inputVector = new Vector3(quaternion.x, quaternion.y, quaternion.z);
        float inputScalar = quaternion.w;

        Vector3 outputVector = Mathf.Exp(inputScalar) * (inputVector.normalized * Mathf.Sin(inputVector.magnitude));
        float outputScalar = Mathf.Exp(inputScalar) * Mathf.Cos(inputVector.magnitude);

        return new Quaternion(outputVector.x, outputVector.y, outputVector.z, outputScalar);
    }

    public static float Magnitude(this Quaternion quaternion)
    {
        return Mathf.Sqrt(quaternion.x * quaternion.x +
                          quaternion.y * quaternion.y +
                          quaternion.z * quaternion.z +
                          quaternion.w * quaternion.w);
    }

    public static Quaternion ScalarMultiply(this Quaternion quaternion, float scalar)
    {
        return new Quaternion(
            quaternion.x * scalar,
            quaternion.y * scalar,
            quaternion.z * scalar,
            quaternion.w * scalar);
    }
}