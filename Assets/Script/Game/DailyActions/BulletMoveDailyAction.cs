using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveDailyAction : DailyAction
{
    public override BehaviorTree Create()
    {
        BehaviorTree behaviorTree = new BehaviorTree();

        ContinueBehavior continueMove = new ContinueBehavior(-1);
        MoveBehavior moveBehavior = new MoveBehavior();
        MoveBehavior.MoveBehaviorInfo moveBehaviorInfo = new MoveBehavior.MoveBehaviorInfo();
        moveBehaviorInfo.speed = 1;
        moveBehaviorInfo.dir = Vector3.right;
        moveBehaviorInfo.targer = mapObject;
        moveBehavior.Enviorment = moveBehaviorInfo;
        continueMove.AddBehavior(moveBehavior);
        behaviorTree.AddBehavior("move", continueMove, BehaviorTree.NodeType.ParallelOr);

        ContinueBehavior continueTouchZombie = new ContinueBehavior(-1);
        TouchZombieBehavior touchZombieBehavior = new TouchZombieBehavior();
        TouchZombieBehavior.TouchZombieBehaviorInfo touchZombieInfo = new TouchZombieBehavior.TouchZombieBehaviorInfo();
        touchZombieInfo.dir = Vector3.right;
        touchZombieInfo.distance = 1f;
        touchZombieInfo.mapObject = mapObject;
        touchZombieBehavior.Enviorment = touchZombieInfo;
        continueTouchZombie.AddBehavior(touchZombieBehavior);
        behaviorTree.AddBehavior("TouchZombie", continueTouchZombie, BehaviorTree.NodeType.ParallelOr);


        ContinueBehavior continueTouchWall = new ContinueBehavior(-1);
        TouchWallBehavior touchWallBehavior = new TouchWallBehavior();
        TouchWallBehavior.TouchBehaviorInfo touchWallInfo = new TouchWallBehavior.TouchBehaviorInfo();
        touchWallInfo.dir = Vector3.right;
        touchWallInfo.distance = 1f;
        touchWallInfo.mapObject = mapObject;
        touchWallBehavior.Enviorment = touchWallInfo;
        continueTouchWall.AddBehavior(touchWallBehavior);
        behaviorTree.AddBehavior("touchWall", continueTouchWall, BehaviorTree.NodeType.Serial);

        SingleNodeBehavior singleNodeBehavior = new SingleNodeBehavior();
        BulletAttackBehavior plantAttackBehavior = new BulletAttackBehavior();
        BulletAttackBehavior.BulletAttackBehaviorInfo plantAttackBehaviorInfo = new BulletAttackBehavior.BulletAttackBehaviorInfo();
        plantAttackBehaviorInfo.Attack = 1;
        plantAttackBehavior.Enviorment = plantAttackBehaviorInfo;
        singleNodeBehavior.AddBehavior(plantAttackBehavior);
        behaviorTree.AddBehavior("attack", singleNodeBehavior, BehaviorTree.NodeType.Serial);


        return behaviorTree;
    }
}
