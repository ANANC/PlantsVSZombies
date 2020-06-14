using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZombieMoveDailyAction : DailyAction
{
    public override BehaviorTree Create()
    {
        BehaviorTree behaviorTree = new BehaviorTree();

        SingleNodeBehavior singleNodeBehavior = new SingleNodeBehavior();
        AddZombieMoveBehavior addZombieMoveBehavior = new AddZombieMoveBehavior();
        AddZombieMoveBehavior.AddZombieMoveBehaviorInfo zombieMoveBehaviorInfo = new AddZombieMoveBehavior.AddZombieMoveBehaviorInfo();
        zombieMoveBehaviorInfo.mapObject = mapObject;
        addZombieMoveBehavior.Enviorment = zombieMoveBehaviorInfo;
        singleNodeBehavior.AddBehavior(addZombieMoveBehavior);
        behaviorTree.AddBehavior("add", singleNodeBehavior, BehaviorTree.NodeType.Serial);

        return behaviorTree;
    }
}