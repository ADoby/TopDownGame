﻿using UnityEngine;
using System.Collections;

public class PriorityWorker
{
    public bool Canceled = false;
    public void Cancel()
    {
        Canceled = true;
    }
    public virtual void Work()
    {

    }
}

public class PriorityWorkerQueue : MonoBehaviour 
{

	private static PriorityWorkerQueue instance;
	public static PriorityWorkerQueue Instance
	{
		get
		{
			if (instance == null)
				instance = FindObjectOfType<PriorityWorkerQueue>();
			return instance;
		}
	}

	public static void AddWorker(int priority, PriorityWorker worker)
	{
        lock (fastLock)
        {
            queue.Enqueue(priority, worker);
        }
	}

    private static System.Object fastLock = new System.Object();

	public static PriorityQueue<int, PriorityWorker> queue = new PriorityQueue<int, PriorityWorker>();

	public int WorkEveryXFrames = 2;
	public int WorkerPerUpdate = 1;

	private int frameCounter = 0;
	
	// Update is called once per frame
	void Update ()
	{
		frameCounter = Mathf.Max(frameCounter-1, 0);
		if(frameCounter == 0)
		{
            using (var tryLock = new TryLock(fastLock))
            {
                if (tryLock.HasLock)
                {
                    if (!queue.IsEmpty)
                    {
                        PriorityWorker worker;

                        for (int i = 0; i < WorkerPerUpdate; i++)
                        {
                            worker = queue.Dequeue();
                            worker.Work();

                            if (queue.IsEmpty)
                                break;
                        }
                    }
                    frameCounter = WorkEveryXFrames;
                }
            }
		}
	}
}