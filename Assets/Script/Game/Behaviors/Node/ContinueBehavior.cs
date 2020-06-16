using UnityEngine;
using UnityEditor;

public class ContinueBehavior : NodeBehavior
{
    private float CurTime;
    private float CurIntervalTimer;

    private float IntervalTime;
    private float FinishTime;

    public ContinueBehavior(float finishTime)
    {
        IntervalTime = Time.deltaTime * GameDefine.FrameValue;
        FinishTime = finishTime;
    }

    public override void Enter()
    {
        CurTime = 0;
        CurIntervalTimer = 0;

        IntervalTime *= GameDefine.FrameValue;
        if (FinishTime != -1)
        {
            FinishTime *= GameDefine.FrameValue;
        }

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