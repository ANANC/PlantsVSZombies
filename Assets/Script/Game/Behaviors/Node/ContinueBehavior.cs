using UnityEngine;
using UnityEditor;

public class ContinueBehavior : NodeBehavior
{
    private float CurTime;

    private float FinishTime;

    public ContinueBehavior(float frameValue)
    {
        FinishTime = frameValue;
    }

    public override void Enter()
    {
        CurTime = 0;

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

        ExecuteLogics();
    }
}