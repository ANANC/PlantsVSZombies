using UnityEngine;
using UnityEditor;

public class SingleNodeBehavior : NodeBehavior
{
    public override void Enter()
    {
        EnterLogics();
    }

    public override void Execute()
    {
        Complete = true;
        ExecuteLogics();
    }
}