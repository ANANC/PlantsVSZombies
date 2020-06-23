using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttackBehavior : LogicBehavior
{
    public class BulletAttackBehaviorInfo : LogicBehaviorInfo
    {
        public MapObject mapObject;
        public int Attack;
    }

    private BulletAttackBehaviorInfo Info;
    private MapObject Zombie;

    public override void Enter()
    {
        Info = (BulletAttackBehaviorInfo)Enviorment;

        TouchZombieBehavior.TouchZombieBehaviorEnvironmentInfo touchPlantBehaviorEnvironmentInfo = Node.BehaviorTree.Environment.Get<TouchZombieBehavior.TouchZombieBehaviorEnvironmentInfo>();
        if (touchPlantBehaviorEnvironmentInfo != null)
        {
            Zombie = touchPlantBehaviorEnvironmentInfo.Zombie;
        }
    }

    public override void Execute()
    {
        Node.Complete = true;

        if (Zombie != null)
        {

            MapOjectAttribute bulletAttribute = Info.mapObject.GetAttribute<MapOjectAttribute>();
            if (bulletAttribute.Hp != 0)
            {
                MapOjectAttribute mapOjectAttribute = Zombie.GetAttribute<MapOjectAttribute>();
                mapOjectAttribute.Hp -= Info.Attack;

                bulletAttribute.Hp = 0;
            }

            AttachAttackAttribute attachAttackBuff = Info.mapObject.GetAttribute<AttachAttackAttribute>();
            if (attachAttackBuff != null)
            {
                if(attachAttackBuff.Fire > 0)
                {
                    GlobalEnvironment.Instance.Get<BuffManager>().AddBuff(Zombie, new AttachAttackBuff());
                }
            }
        }

    }
}
