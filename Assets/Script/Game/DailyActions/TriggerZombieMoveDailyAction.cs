using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZombieMoveDailyAction : DailyAction
{
    public override BehaviorTree Create()
    {
        BehaviorTree behaviorTree = new BehaviorTree();

        SingleNodeBehavior singleNodeBehavior = new SingleNodeBehavior();
        AddDailyActionBehavior addZombieMoveBehavior = new AddDailyActionBehavior();
        AddDailyActionBehavior.AddDailyActionBehaviorInfo zombieMoveBehaviorInfo = new AddDailyActionBehavior.AddDailyActionBehaviorInfo();
        zombieMoveBehaviorInfo.mapObject = mapObject;
        zombieMoveBehaviorInfo.dailyAction = new ZombieMoveDailyAction();
        addZombieMoveBehavior.Enviorment = zombieMoveBehaviorInfo;
        singleNodeBehavior.AddBehavior(addZombieMoveBehavior);
        behaviorTree.AddBehavior("add", singleNodeBehavior, BehaviorTree.NodeType.Serial);

        return behaviorTree;
    }
}