using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletSkill : Skill
{
    public override BehaviorTree Create()
    {
        BehaviorTree behaviorTree = new BehaviorTree();

        CreateFireBulletBehavior createMapObjectBehavior = new CreateFireBulletBehavior();
        CreateFireBulletBehavior.CreateFireBulletInfo createBehaviorInfo = new CreateFireBulletBehavior.CreateFireBulletInfo();
        createBehaviorInfo.Position = mapObject.GetAttribute<MapOjectAttribute>().Position + new Vector3(0, 2, 0);
        createBehaviorInfo.ResourcePath = GameDefine.Path.Bullet;
        createMapObjectBehavior.Enviorment = createBehaviorInfo;
        SingleBehavior singleBehavior = new SingleBehavior();
        singleBehavior.LogicBehavior = createMapObjectBehavior;
        createMapObjectBehavior.Node = singleBehavior;
        behaviorTree.AddBehavior<SingleBehavior>("create", singleBehavior, BehaviorTree.NodeType.Serial);


        return behaviorTree;
    }
}



public class CreateFireBulletBehavior : LogicBehavior
{
    public class CreateFireBulletInfo : LogicBehaviorInfo
    {
        public string ResourcePath;
        public Vector3 Position;
    }

    private CreateFireBulletInfo Info;

    public override void Enter()
    {
        Info = (CreateFireBulletInfo)Enviorment;
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

        BulletMoveDailyAction moveDailyAction = new BulletMoveDailyAction();
        moveDailyAction.mapObject = mapObject;
        GlobalEnvironment.Instance.Get<DailyManager>().RegisterDailyAction<BulletMoveDailyAction>(moveDailyAction);
        GlobalEnvironment.Instance.Get<RepresentManager>().RegisterMapObject(mapObject);
    }

}


