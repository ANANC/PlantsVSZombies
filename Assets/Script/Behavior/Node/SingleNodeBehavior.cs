using UnityEngine;
using UnityEditor;

public class SingleNodeBehavior : NodeBehavior
{
    public override void Execute()
    {
        Complete = true;
    }
}