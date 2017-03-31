using MyHalp;
using UnityEngine;

public class MyThreadingExample : MyComponent
{
    private bool _working;
    private bool _locked;
    private MyJobQueue _queue;

    protected override void OnInit()
    {
        // initialize dispatcher
        MyDispatcher.Init();
    }

    private void OnGUI()
    {
        if (_working)
        {
            GUILayout.Label("Progress: " + (_queue.GetProgress() * 100.0f).ToString("f1") + "%");
        }

        if (GUILayout.Button("Start Simple")) // SIMPLE
        {
            StartSimple();
        }

        if (GUILayout.Button("Start Advanced")) // ADVANCED
        {
            StartAdvanced();
        }

        if (GUILayout.Button("Unlock"))
        {
            _locked = false;
        }
    }

    private void StartSimple()
    {
        // create new job queue
        var queue = MyJobQueue.Create(delegate
        {
            // this is the 'on done', this is called at the very end of the queue when all jobs are done
            Debug.Log("Queue done!");
        });

        queue.Queue(delegate
        {
            Debug.Log("Hello!");
        });

        // start executing the queue
        queue.Execute();
    }

    private void StartAdvanced()
    {
        _working = true;
        _locked = true;

        // create new job queue
        _queue = MyJobQueue.Create(delegate
        {
            _working = false;

            // this is the 'on done', this is called at the very end of the queue when all jobs are done
            Debug.Log("Queue done!");
        });

        _queue.Queue(delegate
        {
            // wait until user clicks UNLOCK button, use custom precision(16 ms, about one frame)
            MyJob.Wait(ref _locked, 16);

            Debug.Log("Working!");
        });

        // queue some more and 'heavy' jobs
        for (var i = 0; i < 25; i++)
        {
            // queue new job
            _queue.Queue(delegate
            {
                MyDispatcher.Dispatch(delegate
                {
                    // run this on the main-thread
                    Debug.Log("Hello, from the main thread!");
                });

                // sleep for 0.5 seconds
                MyJob.Wait(0.5f);
            });
        }

        Debug.Log("Starting the execution!");

        // start executing the queue
        _queue.Execute();
    }
}
