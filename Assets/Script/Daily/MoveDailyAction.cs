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
        continueMove.LogicBehavior = moveBehavior;
        behaviorTree.AddBehavior<ContinueBehavior>("move", continueMove, BehaviorTree.NodeType.ParallelOr);


        ContinueBehavior continueTouch = new ContinueBehavior(-1);
        TouchBehavior touchBehavior = new TouchBehavior();
        TouchBehavior.TouchBehaviorInfo touchBehaviorInfo = new TouchBehavior.TouchBehaviorInfo();
        touchBehaviorInfo.dir = Vector3.right;
        touchBehaviorInfo.distance = 1;
        touchBehaviorInfo.layerMask = -1;
        touchBehaviorInfo.follow = mapObject.GetAttribute<MapObjectArtAttribute>().transform;
        touchBehavior.Enviorment = touchBehaviorInfo;
        continueTouch.LogicBehavior = touchBehavior;
        behaviorTree.AddBehavior<ContinueBehavior>("touch", continueTouch, BehaviorTree.NodeType.Serial);


        return behaviorTree;
    }
}
