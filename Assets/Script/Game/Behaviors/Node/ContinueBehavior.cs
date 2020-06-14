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