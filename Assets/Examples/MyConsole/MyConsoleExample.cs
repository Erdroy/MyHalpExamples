using MyHalp;
using UnityEngine;

public class MyConsoleExample : MyComponent
{
    protected override void OnInit()
    {
        // init mylogger
        MySettings.UseDispatchedLogCallback = false;
        MySettings.UseLogCallback = true;

        // init console
        MyConsole.Init();

        // init commands
        MyCommands.Init();

        // say something!
        MyLogger.Add("Hello, World!");
        MyLogger.Add("Ow, shit, error.", MyLoggerLevel.Error);

        MyCommands.Instance.RegisterCommand("default", "exit", delegate
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        });

        MyCommands.Instance.RegisterCommand("default", "test", (string message, int integer) =>
        {
            MyLogger.Add("Message: " + message);
            MyLogger.Add("Integer: " + integer);
        });

        // params will be correct, no need to check
    }

    protected override void OnTick()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MyLogger.Add("Some message", (MyLoggerLevel)Random.Range(0, 5));
        }
    }
}
