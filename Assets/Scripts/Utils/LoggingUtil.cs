using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoggingUtil 
{

    #region Enums
    
    /*
     * Enums are in code human readable strings that start at 0, so at this case Info is 0 and Debug is 1
     * This allows for performant comparisons that are easily understood by someone reading the code.
     */
    public enum LogMessageLevel
    {
        Info,
        Debug,
        DebugError,
        DebugException,
        Warning,
        Error,
        Exception
    }


    #endregion
    private static bool _initialized = false;
    private static LogMessageLevel _outputLogOverride = LogMessageLevel.Error;
    private static LogMessageLevel _defaultDebugLevel = LogMessageLevel.Info;

    private static void Init()
    {
        if (!_initialized)
        {
            if (Debug.isDebugBuild || Application.isEditor)
            {
                _outputLogOverride = _defaultDebugLevel;
            }

            _initialized = true;
        }
    }
    
    public static void SetOutputLogOverride(LogMessageLevel level)
    {
        if (!_initialized)
        {
            Init();
        }

        _outputLogOverride = level;
    }

    /* The console allows for rich text markup if you wish to change the colours or formatting
     check out the documentation https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html */
    public static void LogInfo(string message, string colour = "black")
    {
        if (!_initialized)
        {
            Init();
        }
        
        
        if (_outputLogOverride <= LogMessageLevel.Info)
        {
            Debug.Log(string.Format("<color={0}>{1}</color>", colour, message));
        }
    }
    
    public static void LogDebug(string message, string colour = "black")
    {
        if (!_initialized)
        {
            Init();
        }
        
        
        if (_outputLogOverride <= LogMessageLevel.Debug)
        {
            Debug.Log(string.Format("<color={0}>{1}</color>", colour, message));
        }
    }
    
    public static void LogDebugError(string message, string colour = "purple")
    {
        if (!_initialized)
        {
            Init();
        }
        
        
        if (_outputLogOverride <= LogMessageLevel.DebugError)
        {
            Debug.Log(string.Format("<color={0}>{1}</color>", colour, message));
        }
    }
    
    //Use these for exceptions that are useful in development but that you want to catch and handle silently in release
    public static void LogDebugException(Exception e)
    {
        if (!_initialized)
        {
            Init();
        }
        
        if (_outputLogOverride <= LogMessageLevel.DebugException)
        {
            Debug.LogException(e);
        }
    }
    
    public static void LogWarning(string message, string colour = "orange")
    {
        if (!_initialized)
        {
            Init();
        }
        
        
        if (_outputLogOverride <= LogMessageLevel.Warning)
        {
            Debug.LogWarning(string.Format("<color={0}>{1}</color>", colour, message));
        }
    }
    
    /*By default this level is what is hit in release mode, allowing you to log important errors.
     In a release build you may have a crash/error service that you can send this information to so 
     that you can find and fix issues if you do have a service you would send the information through to 
     it from here */
    public static void LogError(string message, string colour = "red")
    {
        if (!_initialized)
        {
            Init();
        }
        
        string str = UnityEngine.StackTraceUtility.ExtractStackTrace ();
        
        if (_outputLogOverride <= LogMessageLevel.Error)
        {
            Debug.LogError(string.Format("<color={0}>{1}</color>", colour, message));
        }
    }
    
    //You always want to print exceptions
    public static void LogException(Exception e)
    {
        if (!_initialized)
        {
            Init();
        }

        Debug.LogException(e);
    }

}
