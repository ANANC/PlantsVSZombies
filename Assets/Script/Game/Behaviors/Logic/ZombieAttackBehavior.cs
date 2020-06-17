using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackBehavior : LogicBehavior
{

    public class ZombieAttackBehaviorInfo : LogicBehaviorInfo
    {
        public MapObject mapObject;
        public int Attack;
    }

    private ZombieAttackBehaviorInfo Info;
    private MapObject Plant;

    public override void Enter()
    {
        Info = (ZombieAttackBehaviorInfo)Enviorment;

        TouchPlantBehavior.TouchPlantBehaviorEnvironmentInfo touchPlantBehaviorEnvironmentInfo = Node.BehaviorTree.Environment.Get<TouchPlantBehavior.TouchPlantBehaviorEnvironmentInfo>();
        if(touchPlantBehaviorEnvironmentInfo != null)
        {
            Plant = touchPlantBehaviorEnvironmentInfo.Plant;
        }
    }

    public override void Execute()
    {
        if (Plant == null || Plant.IsActive() == false)
        {
            Node.Complete = true;
        }
        else
        {
            MapOjectAttribute mapOjectAttribute = Plant.GetAttribute<MapOjectAttribute>();
            mapOjectAttribute.Hp -= Info.Attack;
            if (mapOjectAttribute.Hp <= 0)
            {
                Node.Complete = true;
            }
        }

    }
}
