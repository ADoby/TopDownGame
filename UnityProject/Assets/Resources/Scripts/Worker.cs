﻿
public class Worker : Entity
{
    private Job currentJob;
    public Job CurrentJob
    {
        get
        {
            return currentJob;
        }
    }

    public override void Reset()
    {
        base.Reset();
        currentJob = null;
    }

    public virtual void SetJob(Job value)
    {
        currentJob = value;
    }
    public virtual void Update()
    {
        if (currentJob != null && currentJob.Update())
        {
            currentJob = currentJob.NextJob;
        }
    }
}

