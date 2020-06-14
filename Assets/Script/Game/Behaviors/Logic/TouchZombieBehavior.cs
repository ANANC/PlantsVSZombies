using UnityEngine;
using UnityEditor;

public class TouchZombieBehavior : LogicBehavior
{
    public class TouchBehaviorInfo : LogicBehaviorInfo
    {
        public MapObject mapObject;
        public Vector3 dir;
        public float distance;
        public int Hurt;
    }

    private Transform follow;
    private RaycastHit hitInfo;
    private int layerMask;
    private TouchBehaviorInfo Info;

    public override void Enter()
    {
        Info = (TouchBehaviorInfo)Enviorment;
        layerMask = 1 << LayerMask.NameToLayer(GameDefine.Layer.Zombie);
        follow = Info.mapObject.GetAttribute<MapObjectArtAttribute>().transform;
    }

    public override void Execute()
    {
        if (Physics.Raycast(follow.position, Info.dir, out hitInfo, Info.distance, layerMask))
        {
            Debug.Log("Touch Zombie!");

            Node.Complete = true;

            Transform touch = hitInfo.collider.transform;

            Info.mapObject.GetAttribute<MapOjectAttribute>().Hp = 0;

            MapObject zombie = GlobalEnvironment.Instance.Get<MapObjectManager>().GetMapObject(touch);
            if (zombie != null)
            {
                zombie.GetAttribute<MapOjectAttribute>().Hp -= Info.Hurt;
            }

        }
    }
}