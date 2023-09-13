using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            lock (Lock)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                }
                return instance;
            }
        }
    }

    private static readonly object Lock = new object();
}

public class Singleton<T> where T : new()
{
    private static T instance;
    
    public static T Instance
    {
        get
        {
            lock (Lock)
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
    }
    
    private static readonly object Lock = new object();
}