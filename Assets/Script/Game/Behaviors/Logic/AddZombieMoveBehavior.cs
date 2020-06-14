using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddZombieMoveBehavior : LogicBehavior
{
    public class AddZombieMoveBehaviorInfo : LogicBehaviorInfo
    {
        public MapObject mapObject;
    }

    private AddZombieMoveBehaviorInfo Info;
    public override void Enter()
    {
        Info = (AddZombieMoveBehaviorInfo)Enviorment;
    }

    public override void Execute()
    {
        MapOjectAttribute mapOjectAttribute = Info.mapObject.GetAttribute<MapOjectAttribute>();
        if(mapOjectAttribute.Hp <= 0)
        {
            return;
        }

        DailyManager dailyManager = GlobalEnvironment.Instance.Get<DailyManager>();
        dailyManager.RegisterDailyAction(Info.mapObject, new ZombieMoveDailyAction());
    }
}
