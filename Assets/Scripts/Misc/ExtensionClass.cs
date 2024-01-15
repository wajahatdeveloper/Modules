using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;

public static class ExtensionClass
{
    // Convert Vector2 to string
    public static string Vector2ToString(Vector2 vector)
    {
        return vector.x.ToString() + "," + vector.y.ToString();
    }

    // Convert string to Vector2
    public static Vector2 StringToVector2(string str)
    {
        str = str.ReplaceByEmpty("(", ")");
        string[] parts = str.Split(',');
        
        if (parts.Length == 2 && float.TryParse(parts[0], out float x) && float.TryParse(parts[1], out float y))
        {
            return new Vector2(x, y);
        }
        
        Debug.LogError("Invalid string format for Vector2: " + str);
        return Vector2.zero; // Return a default value if parsing fails
    }
    
    public static string Compress(string input)
    {
        byte[] inputData = System.Text.Encoding.UTF8.GetBytes(input);
        using (MemoryStream compressedStream = new MemoryStream())
        {
            using (DeflateStream deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress))
            {
                deflateStream.Write(inputData, 0, inputData.Length);
            }
            return Convert.ToBase64String(compressedStream.ToArray());
        }
    }
    
    public static string Decompress(string input)
    {
        byte[] compressedData = Convert.FromBase64String(input);
        using (MemoryStream decompressedStream = new MemoryStream())
        {
            using (MemoryStream compressedStream = new MemoryStream(compressedData))
            using (DeflateStream deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                deflateStream.CopyTo(decompressedStream);
            }
            return System.Text.Encoding.UTF8.GetString(decompressedStream.ToArray());
        }
    }
    
    public static T[] FindComponentsOfTypeInHierarchy<T>(GameObject rootObject) where T : Component
    {
        if (rootObject == null)
        {
            Debug.LogError("Root GameObject is null.");
            return new T[0];
        }

        List<T> results = new List<T>();
        FindComponentsInHierarchyRecursive(rootObject.transform, results);
        return results.ToArray();
    }

    private static void FindComponentsInHierarchyRecursive<T>(Transform parent, List<T> results) where T : Component
    {
        T component = parent.GetComponent<T>();
        if (component != null)
        {
            results.Add(component);
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            FindComponentsInHierarchyRecursive(child, results);
        }
    }
}