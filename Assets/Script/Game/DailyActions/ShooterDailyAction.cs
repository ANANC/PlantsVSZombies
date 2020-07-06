using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterDailyAction : DailyAction
{
    public override BehaviorTree Create()
    {
        BehaviorTree behaviorTree = new BehaviorTree();

        MapObjectAttribute mapOjectAttribute = mapObject.GetAttribute<MapObjectAttribute>();

        IntervalBehavior intervalBehavior = new IntervalBehavior(10, -1);
        TouchZombieBehavior touchZombieBehavior = new TouchZombieBehavior();
        TouchZombieBehavior.TouchZombieBehaviorInfo touchZombieInfo = new TouchZombieBehavior.TouchZombieBehaviorInfo();
        touchZombieInfo.dir = Vector3.right;
        touchZombieInfo.distance = GameDefine.Art.GardenCellSize.x * (GameDefine.Garden.GardenWidth - mapOjectAttribute.Position.x + 0.5f);
        touchZombieInfo.mapObject = mapObject;
        touchZombieBehavior.Enviorment = touchZombieInfo;
        intervalBehavior.AddBehavior(touchZombieBehavior);
        behaviorTree.AddBehavior("TouchZombie", intervalBehavior, BehaviorTree.NodeType.Serial);


        SingleNodeBehavior singleNodeBehavior = new SingleNodeBehavior();

        UseSkillBehavior useSkillBehavior = new UseSkillBehavior();
        UseSkillBehavior.UseSkillBehaviorInfo useSkillBehaviorInfo = new UseSkillBehavior.UseSkillBehaviorInfo();
        useSkillBehaviorInfo.mapObject = mapObject;
        useSkillBehaviorInfo.skill = new FireBulletSkill();
        useSkillBehavior.Enviorment = useSkillBehaviorInfo;
        singleNodeBehavior.AddBehavior(useSkillBehavior);

        AddDailyActionBehavior addDailyActionBehavior = new AddDailyActionBehavior();
        AddDailyActionBehavior.AddDailyActionBehaviorInfo addDailyActionBehaviorInfo = new AddDailyActionBehavior.AddDailyActionBehaviorInfo();
        addDailyActionBehaviorInfo.mapObject = mapObject;
        addDailyActionBehaviorInfo.dailyAction = new TriggerShooterDailyAction();
        addDailyActionBehavior.Enviorment = addDailyActionBehaviorInfo;
        singleNodeBehavior.AddBehavior(addDailyActionBehavior);

        behaviorTree.AddBehavior("skill", singleNodeBehavior, BehaviorTree.NodeType.Serial);

        return behaviorTree;
    }

}
