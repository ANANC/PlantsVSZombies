using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMoveDailyAction : DailyAction
{
    public override BehaviorTree Create()
    {
        BehaviorTree behaviorTree = new BehaviorTree();

        ContinueBehavior continueMove = new ContinueBehavior(-1);
        MoveBehavior moveBehavior = new MoveBehavior();
        MoveBehavior.MoveBehaviorInfo moveBehaviorInfo = new MoveBehavior.MoveBehaviorInfo();
        moveBehaviorInfo.speed = mapObject.GetAttribute<MapObjectArtAttribute>().MaxSpeed + 0.1f;
        moveBehaviorInfo.dir = Vector3.left;
        moveBehaviorInfo.targer = mapObject;
        moveBehavior.Enviorment = moveBehaviorInfo;
        continueMove.AddBehavior(moveBehavior);
        behaviorTree.AddBehavior("move", continueMove, BehaviorTree.NodeType.ParallelOr);


        ContinueBehavior continueTouchWall = new ContinueBehavior(-1);
        TouchWallBehavior touchWallBehavior = new TouchWallBehavior();
        TouchWallBehavior.TouchBehaviorInfo touchWallInfo = new TouchWallBehavior.TouchBehaviorInfo();
        touchWallInfo.dir = Vector3.left;
        touchWallInfo.distance = 1.6f;
        touchWallInfo.mapObject = mapObject;
        touchWallBehavior.Enviorment = touchWallInfo;
        continueTouchWall.AddBehavior(touchWallBehavior);
        behaviorTree.AddBehavior("touchWall", continueTouchWall, BehaviorTree.NodeType.ParallelOr);


        ContinueBehavior continueTouchZombie = new ContinueBehavior(-1);
        TouchPlantBehavior touchPlantBehavior = new TouchPlantBehavior();
        TouchPlantBehavior.TouchBehaviorInfo touchPlantInfo = new TouchPlantBehavior.TouchBehaviorInfo();
        touchPlantInfo.dir = Vector3.left;
        touchPlantInfo.distance = 1.6f;
        touchPlantInfo.mapObject = mapObject;
        touchPlantBehavior.Enviorment = touchPlantInfo;
        continueTouchZombie.AddBehavior(touchPlantBehavior);
        behaviorTree.AddBehavior("TouchPlant", continueTouchZombie, BehaviorTree.NodeType.Serial);


        IntervalBehavior intervalBehavior = new IntervalBehavior(5, -1);
        ZombieAttackBehavior zombieAttackBehavior = new ZombieAttackBehavior();
        ZombieAttackBehavior.ZombieAttackBehaviorInfo zombieAttackInfo = new ZombieAttackBehavior.ZombieAttackBehaviorInfo();
        zombieAttackInfo.mapObject = mapObject;
        zombieAttackInfo.Attack = 1;
        zombieAttackBehavior.Enviorment = zombieAttackInfo;
        intervalBehavior.AddBehavior(zombieAttackBehavior);
        behaviorTree.AddBehavior("attack", intervalBehavior, BehaviorTree.NodeType.Serial);


        SingleNodeBehavior singleNodeBehavior = new SingleNodeBehavior();
        AddDailyActionBehavior addDailyActionBehavior = new AddDailyActionBehavior();
        AddDailyActionBehavior.AddDailyActionBehaviorInfo addDailyActionBehaviorInfo = new AddDailyActionBehavior.AddDailyActionBehaviorInfo();
        addDailyActionBehaviorInfo.mapObject = mapObject;
        addDailyActionBehaviorInfo.dailyAction = new TriggerZombieMoveDailyAction();
        addDailyActionBehavior.Enviorment = addDailyActionBehaviorInfo;
        singleNodeBehavior.AddBehavior(addDailyActionBehavior);
        behaviorTree.AddBehavior("daily", singleNodeBehavior, BehaviorTree.NodeType.Serial);


        return behaviorTree;
    }

}
