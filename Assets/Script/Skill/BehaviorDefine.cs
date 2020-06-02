using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------------------------- Node --------------------------------------

public class SingleBehavior : NodeBehavior
{
    public override void Execute()
    {
        Complete = true;
    }
}


public class ContinueBehavior:NodeBehavior
{
    private float CurTime;
    private float FinishTime;

    public ContinueBehavior(float finishTime)
    {
        FinishTime = finishTime;
    }

    public override void Enter()
    {
        CurTime = 0;
    }

    private bool TimeContinue()
    {
        CurTime += 1;
        return CurTime > FinishTime;
    }

    public override void Execute()
    {
        if (FinishTime == -1)
        {
            Complete = false;
        }
        else
        {
            Complete = TimeContinue();
        }
    }
}

//----------------------------- Logic --------------------------------------

public class CreateMapObjectBehavior : LogicBehavior
{
    public class CreateBehaviorInfo : LogicBehaviorInfo
    {
        public string ResourcePath;
        public Vector3 Position;
    }

    public class CreateBehaviorGlobalInfo : IBehaviorEnvironmentInfo
    {
        public MapObject mapObject;
    }

    private CreateBehaviorInfo Info;

    public override void Enter()
    {
        Info = (CreateBehaviorInfo)Enviorment;
        
    }

    public override void Execute()
    {
        MapObject mapObject = GlobalEnvironment.Instance.Get<MapObjectManager>().InstanceMapObject();

        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(Info.ResourcePath);
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;

        MapOjectAttribute mapObjectAttribute = mapObject.GetAttribute<MapOjectAttribute>();
        mapObjectAttribute.Position = Info.Position;


        CreateBehaviorGlobalInfo createBehaviorGlobalInfo = new CreateBehaviorGlobalInfo();
        createBehaviorGlobalInfo.mapObject = mapObject;
        GlobalEnvironmentInfo.Add<CreateBehaviorGlobalInfo>(createBehaviorGlobalInfo);

    }

}


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
        Info.targer.GetAttribute<MapOjectAttribute>().Position += Info.dir * Info.speed;
    }
}

public class ProcessTargetMoveBehavior : LogicBehavior
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
        Info.targer = GlobalEnvironmentInfo.Get<CreateMapObjectBehavior.CreateBehaviorGlobalInfo>().mapObject;
    }

    public override void Execute()
    {
        Info.targer.GetAttribute<MapOjectAttribute>().Position += Info.dir * Info.speed;
    }
}



public class TouchBehavior :LogicBehavior
{
    public class TouchBehaviorInfo : LogicBehaviorInfo
    {
        public Transform follow;
        public Vector3 dir;
        public float distance;
        public int layerMask;
    }

    public class TouchBehaviorGlobalInfo :IBehaviorEnvironmentInfo
    {
        public Transform touch;
    }

    private TouchBehaviorInfo Info;

    public override void Enter()
    {
        Info = (TouchBehaviorInfo)Enviorment;
        GlobalEnvironmentInfo.Add<TouchBehaviorGlobalInfo>(new TouchBehaviorGlobalInfo());
    }

    public override void Execute()
    {
        TouchBehaviorGlobalInfo touchGlobalInfo = GlobalEnvironmentInfo.Get<TouchBehaviorGlobalInfo>();
        bool touch = TouchOther(Info.follow, Info.dir, Info.distance, Info.layerMask, out touchGlobalInfo.touch);
        if(touch)
        {
            NodeBehavior.Complete = NodeBehavior.Complete || touch;
        }
    }

    public bool TouchOther(Transform follow, Vector3 dir, float distance, int layerMask, out Transform touch)
    {
        touch = null;

        RaycastHit hitInfo;
        if (Physics.Raycast(follow.position, dir, out hitInfo, distance, layerMask))
        {
            touch = hitInfo.collider.transform;
            return true;
        }
        return false;
    }
}


