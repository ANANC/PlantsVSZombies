using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalBehavior : NodeBehavior
{
    private float CurTime;
    private float CurIntervalTimer;

    private float IntervalTime;
    private float FinishTime;

    public IntervalBehavior(float interval, float finishTime)
    {
        IntervalTime = interval;
        FinishTime = finishTime;
    }

    public override void Enter()
    {
        CurTime = 0;
        CurIntervalTimer = 0;
        EnterLogics();
    }

    public override void Execute()
    {
        if (FinishTime == -1)
        {
            Complete = false;
        }
        else
        {
            CurTime += Time.deltaTime;
            Complete = CurTime > FinishTime;
        }

        CurIntervalTimer += Time.deltaTime;
        if (CurIntervalTimer >= IntervalTime)
        {
            CurIntervalTimer = 0;
            ExecuteLogics();
        }
    }
}
