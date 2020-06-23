using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachAttackAttribute : IAttribute
{
    public int Fire;

    public void Init()
    {
        Fire = 0;
    }

    public void UnInit()
    {
        Init();
    }
}
