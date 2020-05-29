using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehavior {
     void Enter();
     void Exist();
}

public class ContinueBehavior: IBehavior
{
    private int m_CurTime;

    public void Enter()
    {
        m_CurTime = 0;
    }

    public void Exist()
    {

    }

    public bool TimeContinue(float finishTime)
    {
        m_CurTime += 1;
        return m_CurTime > finishTime;
    }
}

public class TouchBehavior : IBehavior {
    public void Enter()
    {

    }
    public void Exist()
    {

    }

    public bool TouchOther(Transform follow, Vector3 dir, float distance, int layerMask, out Transform touch)
    {
        touch = null;

        RaycastHit hitInfo;
        if (Physics.Raycast(follow.position, dir, out hitInfo, distance, layerMask))
        {
            touch = hitInfo.collider.transform;

            return true;
        }
        return false;
    }
}


