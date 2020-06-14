using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DailyAction 
{
    public MapObject mapObject;
    private BehaviorTree behaviorTree;

    public void Init()
    {
        behaviorTree = Create();
        behaviorTree.Execute();
    }
    public void UnInit() {}

    public abstract BehaviorTree Create();

    public bool Complete()
    {
        if (behaviorTree == null)
        {
            return true;
        }

        return behaviorTree.Complete();
    }
}
