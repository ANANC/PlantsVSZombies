using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehavior {
     void Enter();
     void Exist();
     bool Execute();
}

public class ContinueBehavior: IBehavior
{
    private float CurTime;
    private float FinishTime;

    public ContinueBehavior(float finishTime)
    {
        FinishTime = finishTime;
    }

    public void Enter()
    {
        CurTime = 0;
    }

    public void Exist()
    {

    }

    public bool Execute()
    {
        if(FinishTime == -1)
        {
            return false;
        }
        return TimeContinue();
    }

    private bool TimeContinue()
    {
        CurTime += 1;
        return CurTime > FinishTime;
    }
}

public class TouchBehavior : IBehavior
{
    private bool Touch;
    public void Enter()
    {
        Touch = false;
    }
    public void Exist()
    {

    }

    public bool Execute()
    {
        return Touch;
    }

    public static void TouchOther(TouchBehavior behaviour,Transform follow, Vector3 dir, float distance, int layerMask, out Transform touch)
    {
        touch = null;
        behaviour.Touch = false;

        RaycastHit hitInfo;
        if (Physics.Raycast(follow.position, dir, out hitInfo, distance, layerMask))
        {
            touch = hitInfo.collider.transform;

            behaviour.Touch = true;
        }

    }
}


