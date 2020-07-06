using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlantBehavior : LogicBehavior
{
    public class TouchBehaviorInfo : LogicBehaviorInfo
    {
        public MapObject mapObject;
        public Vector3 dir;
        public float distance;
    }

    public class TouchPlantBehaviorEnvironmentInfo : IBehaviorEnvironmentInfo
    {
        public MapObject Plant;
    }


    private Transform follow;
    private RaycastHit hitInfo;
    private int layerMask;
    private TouchBehaviorInfo Info;

    public override void Enter()
    {
        Info = (TouchBehaviorInfo)Enviorment;
        layerMask = 1 << LayerMask.NameToLayer(GameDefine.Layer.Plant);
        follow = Info.mapObject.GetAttribute<MapObjectArtAttribute>().transform;
    }

    public override void Execute()
    {
        Debug.DrawLine(follow.position, follow.position + Info.dir * Info.distance,Color.red);

        if (Physics.Raycast(follow.position, Info.dir, out hitInfo, Info.distance, layerMask))
        {
           // Debug.Log("Touch Plant!");

            Node.Complete = true;

            Transform touch = hitInfo.collider.transform;
            MapObject plant = GlobalEnvironment.Instance.Get<MapObjectManager>().GetMapObject(touch);
            if (plant != null)
            {
                TouchPlantBehaviorEnvironmentInfo environmentInfo = new TouchPlantBehaviorEnvironmentInfo();
                environmentInfo.Plant = plant;
                Node.BehaviorTree.Environment.Add<TouchPlantBehaviorEnvironmentInfo>(environmentInfo);

                MapObjectAttribute mapOjectAttribute = Info.mapObject.GetAttribute<MapObjectAttribute>();
                mapOjectAttribute.Position = follow.position;
            }

        }
    }
}
