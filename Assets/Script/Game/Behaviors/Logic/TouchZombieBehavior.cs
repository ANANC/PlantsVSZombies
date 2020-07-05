using UnityEngine;
using UnityEditor;

public class TouchZombieBehavior : LogicBehavior
{
    public class TouchZombieBehaviorInfo : LogicBehaviorInfo
    {
        public MapObject mapObject;
        public Vector3 dir;
        public float distance;
    }

    public class TouchZombieBehaviorEnvironmentInfo : IBehaviorEnvironmentInfo
    {
        public MapObject Zombie;
    }


    private Transform follow;
    private RaycastHit hitInfo;
    private int layerMask;
    private TouchZombieBehaviorInfo Info;

    public override void Enter()
    {
        Info = (TouchZombieBehaviorInfo)Enviorment;
        layerMask = 1 << LayerMask.NameToLayer(GameDefine.Layer.Zombie);
        follow = Info.mapObject.GetAttribute<MapObjectArtAttribute>().transform;
    }

    public override void Execute()
    {
        Debug.DrawLine(follow.position, follow.position + Info.dir * Info.distance,Color.blue);

        if (Physics.Raycast(follow.position, Info.dir, out hitInfo, Info.distance, layerMask))
        {
           // Debug.Log("Touch Zombie!");

            Node.Complete = true;

            Transform touch = hitInfo.collider.transform;
            MapObject zombie = GlobalEnvironment.Instance.Get<MapObjectManager>().GetMapObject(touch);

            if (zombie != null)
            {
                TouchZombieBehaviorEnvironmentInfo touchZombieBehaviorEnvironmentInfo = new TouchZombieBehaviorEnvironmentInfo();
                touchZombieBehaviorEnvironmentInfo.Zombie = zombie;
                Node.BehaviorTree.Environment.Add<TouchZombieBehaviorEnvironmentInfo>(touchZombieBehaviorEnvironmentInfo);
            }

        }
    }
}