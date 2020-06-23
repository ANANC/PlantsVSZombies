using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAttachObjectBehavior : LogicBehavior
{
    public class TouchBehaviorInfo : LogicBehaviorInfo
    {
        public int layerMask;
        public MapObject mapObject;
        public Vector3 dir;
        public float radius;
        public float maxDistance;
    }

    public class AddAttachBehaviorEnvironmentInfo : IBehaviorEnvironmentInfo
    {
        public MapObject mapObject;
    }

    private Transform follow;

    private RaycastHit hitInfo;
    private int layerMask;
    private TouchBehaviorInfo Info;


    public override void Enter()
    {
        Info = (TouchBehaviorInfo)Enviorment;
        follow = Info.mapObject.GetAttribute<MapObjectArtAttribute>().transform;
    }

    public override void Execute()
    {
        Debug.DrawLine(follow.position + Info.radius * Info.maxDistance * Vector3.left, follow.position + Info.radius * Info.maxDistance * Vector3.right, Color.yellow);

        if (Physics.SphereCast(follow.position, Info.radius, Info.dir, out hitInfo, Info.maxDistance, layerMask))
        {
            Debug.Log("Touch! Try Add Attach!");

            Node.Complete = true;

            Transform touch = hitInfo.collider.transform;
            MapObject mapObjct = GlobalEnvironment.Instance.Get<MapObjectManager>().GetMapObject(touch);
            if (mapObjct != null)
            {
                AddAttachBehaviorEnvironmentInfo addAttachBehaviorEnvironmentInfo = new AddAttachBehaviorEnvironmentInfo();
                addAttachBehaviorEnvironmentInfo.mapObject = mapObjct;
                Node.BehaviorTree.Environment.Add<AddAttachBehaviorEnvironmentInfo>(addAttachBehaviorEnvironmentInfo);
            }
        }
    }
}
