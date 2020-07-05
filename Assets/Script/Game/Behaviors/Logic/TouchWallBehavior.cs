using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWallBehavior : LogicBehavior
{
    public class TouchBehaviorInfo : LogicBehaviorInfo
    {
        public MapObject mapObject;
        public Vector3 dir;
        public float distance;
    }

    private Transform follow;
    private RaycastHit hitInfo;
    private int layerMask;
    private TouchBehaviorInfo Info;

    public override void Enter()
    {
        Info = (TouchBehaviorInfo)Enviorment;
        layerMask = 1 << LayerMask.NameToLayer(GameDefine.Layer.Wall);
        follow = Info.mapObject.GetAttribute<MapObjectArtAttribute>().transform;
    }

    public override void Execute()
    {
        if (Physics.Raycast(follow.position, Info.dir, Info.distance, layerMask))
        {
           // Debug.Log("Touch Wall!");

            Node.Complete = true;

            Info.mapObject.GetAttribute<MapOjectAttribute>().Hp = 0;
        }
    }

}
