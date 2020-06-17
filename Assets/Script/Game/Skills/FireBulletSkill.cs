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
        createBehaviorInfo.Position = mapObject.GetAttribute<MapObjectArtAttribute>().transform.position + new Vector3(1, 0, 0);
        createMapObjectBehavior.Enviorment = createBehaviorInfo;
        SingleNodeBehavior singleBehavior = new SingleNodeBehavior();
        singleBehavior.AddBehavior(createMapObjectBehavior);
        behaviorTree.AddBehavior("create", singleBehavior, BehaviorTree.NodeType.Serial);


        return behaviorTree;
    }
}

public class CreateFireBulletBehavior : LogicBehavior
{
    public class CreateFireBulletInfo : LogicBehaviorInfo
    {
        public Vector3 Position;
    }

    private CreateFireBulletInfo Info;

    public override void Enter()
    {
        Info = (CreateFireBulletInfo)Enviorment;
    }

    public override void Execute()
    {
        GlobalEnvironment.Instance.Get<GameMapObjectManager>().CreateBullet(Info.Position);
    }

}


