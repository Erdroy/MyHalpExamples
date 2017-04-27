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

        MyCommands.Instance.RegisterCommand("", "volume", (float level) =>
        {
            MyLogger.Add("Volume: " + level);
        }, "Sets the overall game volume");

        MyCommands.Instance.RegisterCommand("", "sfx_volume", (float level) =>
        {
            MyLogger.Add("Volume: " + level);
        }, "Sets the sound effects volume");

        MyCommands.Instance.RegisterCommand("", "vfx_volume", (float level) =>
        {
            MyLogger.Add("Volume: " + level);
        }, "Sets the voice volume");

        MyCommands.Instance.RegisterCommand("", "say", (string message) =>
        {
            MyLogger.Add("Says: " + message);
        }, "Say something through the game chat");

        MyCommands.Instance.RegisterCommand("", "spawn_entity", (string entityName) =>
        {
            MyLogger.Add("Spawned: " + entityName + " id: " + 100);
        }, "Spawns entity by name");

        MyCommands.Instance.RegisterCommand("", "destroy_entity", (int entId) =>
        {
            MyLogger.Add("Destroyed entity with id: " + entId);
        }, "Destroys entity by entity id.");

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
