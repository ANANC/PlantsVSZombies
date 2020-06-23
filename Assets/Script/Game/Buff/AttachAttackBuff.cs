using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachAttackBuff : Buff
{
    public override BehaviorTree Create()
    {
        BehaviorTree behaviorTree = new BehaviorTree();

        IntervalBehavior intervalBehavior = new IntervalBehavior(5, 3);

        AttachAttackBehavior attachAttackBehavior = new AttachAttackBehavior();
        AttachAttackBehavior.AttachAttackBehaviorInfo attachAttackBehaviorInfo = new AttachAttackBehavior.AttachAttackBehaviorInfo();
        attachAttackBehaviorInfo.mapObject = mapObject;
        attachAttackBehavior.Enviorment = attachAttackBehaviorInfo;

        intervalBehavior.AddBehavior(attachAttackBehavior);
        behaviorTree.AddBehavior("attachAttack", intervalBehavior, BehaviorTree.NodeType.Serial);

        return behaviorTree;
    }
}
