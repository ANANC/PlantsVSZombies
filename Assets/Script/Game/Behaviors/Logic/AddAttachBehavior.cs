using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAttachBehavior : LogicBehavior
{
    public class AddAttachBehaviorInfo : LogicBehaviorInfo
    {
        public int Fire;
    }

    private AddAttachBehaviorInfo Info;
    private TouchAttachObjectBehavior.AddAttachBehaviorEnvironmentInfo EnvironmentInfo;

    public override void Enter()
    {
        Info = (AddAttachBehaviorInfo)Enviorment;
        EnvironmentInfo = Node.BehaviorTree.Environment.Get<TouchAttachObjectBehavior.AddAttachBehaviorEnvironmentInfo>();
    }

    public override void Execute()
    {
        if (EnvironmentInfo == null)
        {
            return;
        }

        AttachAttackAttribute attachAttackAttribute = EnvironmentInfo.mapObject.GetAttribute<AttachAttackAttribute>();
        if (attachAttackAttribute == null)
        {
            attachAttackAttribute = new AttachAttackAttribute();
            EnvironmentInfo.mapObject.AddAttribute<AttachAttackAttribute>(attachAttackAttribute.GetType().Name, attachAttackAttribute);
        }

        attachAttackAttribute.Fire = Info.Fire;
    }

}
