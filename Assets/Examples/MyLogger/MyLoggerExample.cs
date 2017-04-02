using System;
using MyHalp;
using UnityEngine;

public class MyLoggerExample : MyComponent
{
    protected override void OnInit()
    {
        // this is optional
        MySettings.UseDispatchedLogCallback = false;
        MySettings.UseLogCallback = true;
        MySettings.ProduceLogFile = true;
        MySettings.LogFile = "log.txt";

        MyLogger.Add("Hello, World!");

        Debug.Log("LOG");
        Debug.LogWarning("WARNING");
        Debug.LogError("ERROR");
        Debug.LogException(new Exception("FATAL"));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("MyLogger.Add()"))
        {
            MyLogger.Add("New log!");
        }

        if (GUILayout.Button("Debug.Log()"))
        {
            Debug.Log("New log using Debug.Log()!");
        }

        if (GUILayout.Button("Threaded MyLogger.Add()"))
        {
            MyJob.Run(delegate
            {
                MyLogger.Add("Threaded new log!");
            });
        }

        if (GUILayout.Button("Threaded Debug.Log()"))
        {
            MyJob.Run(delegate
            {
                Debug.Log("Threaded new log using Debug.Log()!");
            });
        }
        if (GUILayout.Button("Point"))
        {
            MyLogger.Point(); // show point
        }
        if (GUILayout.Button("Threaded Point"))
        {
            MyJob.Run(delegate
            {
                MyLogger.Point(); // show point
            });
        }
    }
}
