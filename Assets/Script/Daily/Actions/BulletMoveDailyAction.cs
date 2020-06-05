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
        moveBehavior.Node = continueMove;
        continueMove.LogicBehavior = moveBehavior;
        behaviorTree.AddBehavior<ContinueBehavior>("move", continueMove, BehaviorTree.NodeType.ParallelOr);

        ContinueBehavior continueTouchZombie = new ContinueBehavior(-1);
        TouchZombieBehavior touchZombieBehavior = new TouchZombieBehavior();
        TouchZombieBehavior.TouchBehaviorInfo touchZombieInfo = new TouchZombieBehavior.TouchBehaviorInfo();
        touchZombieInfo.dir = Vector3.right;
        touchZombieInfo.distance = 1f;
        touchZombieInfo.mapObject = mapObject;
        touchZombieInfo.Hurt = 1;
        touchZombieBehavior.Enviorment = touchZombieInfo;
        touchZombieBehavior.Node = continueTouchZombie;
        continueTouchZombie.LogicBehavior = touchZombieBehavior;
        behaviorTree.AddBehavior<ContinueBehavior>("TouchZombie", continueTouchZombie, BehaviorTree.NodeType.ParallelOr);


        ContinueBehavior continueTouchWall = new ContinueBehavior(-1);
        TouchWallBehavior touchWallBehavior = new TouchWallBehavior();
        TouchWallBehavior.TouchBehaviorInfo touchWallInfo = new TouchWallBehavior.TouchBehaviorInfo();
        touchWallInfo.dir = Vector3.right;
        touchWallInfo.distance = 1f;
        touchWallInfo.mapObject = mapObject;
        touchWallBehavior.Enviorment = touchWallInfo;
        touchWallBehavior.Node = continueTouchWall;
        continueTouchWall.LogicBehavior = touchWallBehavior;
        behaviorTree.AddBehavior<ContinueBehavior>("touchWall", continueTouchWall, BehaviorTree.NodeType.Serial);


        return behaviorTree;
    }
}
