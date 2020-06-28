using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchAttachObjectBehavior : LogicBehavior
{
    public class TouchBehaviorInfo : LogicBehaviorInfo
    {
        public int layerMask;
        public MapObject mapObject;
        public float distance;
    }

    public class AddAttachBehaviorEnvironmentInfo : IBehaviorEnvironmentInfo
    {
        public MapObject mapObject;
    }

    private Transform follow;

    private RaycastHit hitInfo;
    private TouchBehaviorInfo Info;
    private Transform touch;

    private Vector3[] dirs = new Vector3[] 
    {
        Vector3.left,Vector3.right,Vector3.up,Vector3.down,
        new Vector3(1,1,0),new Vector3(1,-1,0),new Vector3(-1,1,0),new Vector3(-1,-1,0)
    };


    public override void Enter()
    {
        Info = (TouchBehaviorInfo)Enviorment;
        follow = Info.mapObject.GetAttribute<MapObjectArtAttribute>().transform;
    }

    public override void Execute()
    {
        touch = null;
        for (int index = 0; index < dirs.Length; index++)
        {
            Vector3 dir = dirs[index];
            Debug.DrawLine(follow.position, follow.position + dir * Info.distance, Color.yellow);

            if (Physics.Raycast(follow.position, dir, out hitInfo, Info.distance, Info.layerMask))
            {
                Debug.Log("Touch! Try Add Attach!");
                touch = hitInfo.collider.transform;
                break;
            }
        }

        if (touch == null)
        {
            return;
        }

        Node.Complete = true;

        MapObject mapObjct = GlobalEnvironment.Instance.Get<MapObjectManager>().GetMapObject(touch);
        if (mapObjct != null)
        {
            AddAttachBehaviorEnvironmentInfo addAttachBehaviorEnvironmentInfo = new AddAttachBehaviorEnvironmentInfo();
            addAttachBehaviorEnvironmentInfo.mapObject = mapObjct;
            Node.BehaviorTree.Environment.Add<AddAttachBehaviorEnvironmentInfo>(addAttachBehaviorEnvironmentInfo);
        }

    }
}
