using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShooterDailyAction : DailyAction
{
    public override BehaviorTree Create()
    {
        BehaviorTree behaviorTree = new BehaviorTree();

        SingleNodeBehavior singleNodeBehavior = new SingleNodeBehavior();
        AddDailyActionBehavior addDailyActionBehavior = new AddDailyActionBehavior();
        AddDailyActionBehavior.AddDailyActionBehaviorInfo addDailyActionBehaviorInfo = new AddDailyActionBehavior.AddDailyActionBehaviorInfo();
        addDailyActionBehaviorInfo.mapObject = mapObject;
        addDailyActionBehaviorInfo.dailyAction = new ShooterDailyAction();
        addDailyActionBehavior.Enviorment = addDailyActionBehaviorInfo;
        singleNodeBehavior.AddBehavior(addDailyActionBehavior);
        behaviorTree.AddBehavior("daily", singleNodeBehavior, BehaviorTree.NodeType.Serial);


        return behaviorTree;
    }
}
