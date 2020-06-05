using UnityEngine;
using UnityEditor;

public class ContinueBehavior : NodeBehavior
{
    private float CurTime;
    private float FinishTime;

    public ContinueBehavior(float finishTime)
    {
        FinishTime = finishTime;
    }

    public override void Enter()
    {
        CurTime = 0;
    }

    private bool TimeContinue()
    {
        CurTime += 1;
        return CurTime > FinishTime;
    }

    public override void Execute()
    {
        if (FinishTime == -1)
        {
            Complete = false;
        }
        else
        {
            Complete = TimeContinue();
        }
    }
}