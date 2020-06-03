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
    }
    public void UnInit()
    {

    }
    public abstract BehaviorTree Create();
    public abstract void Execute();
}
