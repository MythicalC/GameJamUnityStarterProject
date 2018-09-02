using UnityEngine;
using System.Collections;

public class Singleton<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_isExiting)
            {
                return null;
            }

            if (_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }

    private static bool _isExiting = false;

    public static bool IsExiting
    {
        get { return _isExiting; }
    }

    public void CloseInstance()
    {
        _isExiting = true;
    }
}