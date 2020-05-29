using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager 
{
   
    public void FireBullet(MapObject user)
    {
        MapObject bullet = GlobalEnvironment.Instance.Get<MapObjectManager>().InstanceMapObject();

        MapObjectArtAttribute mapObjectArtAttribute = bullet.GetAttribute<MapObjectArtAttribute>();
        GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance("Art/Module/Prefab/Bullet");
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;

        MapOjectAttribute mapObjectAttribute = bullet.GetAttribute<MapOjectAttribute>();
        mapObjectAttribute.Position = user.GetAttribute<MapOjectAttribute>().Position + Vector3.right * 20;

        BehaviorTree behaviorTree = new BehaviorTree();
        behaviorTree.AddBehavior<ContinueBehavior>(new ContinueBehavior(-1), BehaviorTree.NodeType.Parallel, TouchBehavior.TouchOther());

    }
}
