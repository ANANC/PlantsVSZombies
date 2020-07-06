using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDailyActionBehavior : LogicBehavior
{
    public class AddDailyActionBehaviorInfo : LogicBehaviorInfo
    {
        public MapObject mapObject;
        public DailyAction dailyAction;
    }

    private AddDailyActionBehaviorInfo Info;
    public override void Enter()
    {
        Info = (AddDailyActionBehaviorInfo)Enviorment;
    }

    public override void Execute()
    {
        MapObjectAttribute mapOjectAttribute = Info.mapObject.GetAttribute<MapObjectAttribute>();
        if (mapOjectAttribute.Hp <= 0)
        {
            return;
        }

        DailyManager dailyManager = GlobalEnvironment.Instance.Get<DailyManager>();
        dailyManager.RegisterDailyAction(Info.mapObject, Info.dailyAction);
    }
}