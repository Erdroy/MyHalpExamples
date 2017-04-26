using MyHalp;
using UnityEngine;

public class MyConsoleExample : MyComponent
{
    protected override void OnInit()
    {
        // init mylogger
        MySettings.UseDispatchedLogCallback = false;
        MySettings.UseLogCallback = true;

        // init my console
        MyConsole.Init();

        // say something!
        MyLogger.Add("Hello, World!");
        MyLogger.Add("Ow, shit, error.", MyLoggerLevel.Error);
    }

    protected override void OnTick()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MyLogger.Add("Some message", (MyLoggerLevel)Random.Range(0, 5));
        }
    }
}
