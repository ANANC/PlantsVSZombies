using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBehavior : LogicBehavior
{
    public class MoveBehaviorInfo : LogicBehaviorInfo
    {
        public MapObject targer;
        public Vector3 dir;
        public float speed;
    }

    private MoveBehaviorInfo Info;

    public override void Enter()
    {
        Info = (MoveBehaviorInfo)Enviorment;
    }

    public override void Execute()
    {
        Info.targer.GetAttribute<MapObjectAttribute>().Position += Info.dir * Info.speed;
    }
}
