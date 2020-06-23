using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachAttackBehavior : LogicBehavior
{
    public class AttachAttackBehaviorInfo: LogicBehaviorInfo
    {
        public MapObject mapObject;
    }

    private AttachAttackBehaviorInfo Info;

    private MapOjectAttribute mapOjectAttribute;
    private AttachAttackAttribute attachAttackAttribute;

    public override void Enter()
    {
        Info = (AttachAttackBehaviorInfo)Enviorment;

        if (Info.mapObject != null && Info.mapObject.IsActive())
        {
            mapOjectAttribute = Info.mapObject.GetAttribute<MapOjectAttribute>();
            attachAttackAttribute = Info.mapObject.GetAttribute<AttachAttackAttribute>();
        }
    }

    public override void Execute()
    {
        if (attachAttackAttribute == null)
        {
            return;
        }

        mapOjectAttribute.Hp -= attachAttackAttribute.Fire;
    }
}
