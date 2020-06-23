using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchwoodDailyAction : DailyAction
{
    public override BehaviorTree Create()
    {
        BehaviorTree behaviorTree = new BehaviorTree();

        ContinueBehavior continueBehavior = new ContinueBehavior(-1);
        TouchAttachObjectBehavior touchAttachObjectBehavior = new TouchAttachObjectBehavior();
        TouchAttachObjectBehavior.TouchBehaviorInfo touchBehaviorInfo = new TouchAttachObjectBehavior.TouchBehaviorInfo();
        touchBehaviorInfo.layerMask = 1 << LayerMask.NameToLayer(GameDefine.Layer.Bullet);
        touchBehaviorInfo.dir = Vector3.zero;
        touchBehaviorInfo.radius = 1;
        touchBehaviorInfo.maxDistance = 1;
        touchBehaviorInfo.mapObject = mapObject;
        touchAttachObjectBehavior.Enviorment = touchBehaviorInfo;
        continueBehavior.AddBehavior(touchAttachObjectBehavior);

        behaviorTree.AddBehavior("touchBullet", continueBehavior, BehaviorTree.NodeType.Serial);

        SingleNodeBehavior singleNodeBehavior = new SingleNodeBehavior();
        AddAttachBehavior addAttachBehavior = new AddAttachBehavior();
        AddAttachBehavior.AddAttachBehaviorInfo addAttachBehaviorInfo = new AddAttachBehavior.AddAttachBehaviorInfo();
        addAttachBehaviorInfo.Fire = 1;
        addAttachBehavior.Enviorment = addAttachBehaviorInfo;
        singleNodeBehavior.AddBehavior(addAttachBehavior);

        behaviorTree.AddBehavior("addFire", singleNodeBehavior, BehaviorTree.NodeType.Serial);

        return behaviorTree;
    }
}
