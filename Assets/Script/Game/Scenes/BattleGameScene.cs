using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameScene : GameScene
{
    private GardenBattleGamePlay GamePlay;
    private GardenMap GardenMap;

    private MapObjectManager MapObjectMgr;
    private RepresentManager RepresentMgr;
    private SkillManager SkillMgr;
    private DailyManager DailyMgr;

    public override void Init()
    {
        GamePlay = new GardenBattleGamePlay();
        GardenMap = new GardenMap();
    }

    public override void UnInit()
    {

    }

    public override void Enter()
    {
        SkillMgr = GlobalEnvironment.Instance.Get<SkillManager>();
        MapObjectMgr = GlobalEnvironment.Instance.Get<MapObjectManager>();
        RepresentMgr = GlobalEnvironment.Instance.Get<RepresentManager>();
        DailyMgr = GlobalEnvironment.Instance.Get<DailyManager>();

        GardenMap.Enter();
        GamePlay.Enter();

        InitBattleEnvironment();
    }

    public override void Exist()
    {
        GamePlay.Exist();
    }

    public void InitBattleEnvironment()
    {
        CreateShooter(Vector3.zero);

        CreateZombieToMap(GameDefine.Path.Zombie, new Vector3(GardenMap.GardenWidth+1, 0, 0));
       
    }

    public void CreateShooter(Vector3 logicPos)
    {
        MapObject shooter = CreatePlantToMap(GameDefine.Path.Plant, logicPos);
        if(shooter == null)
        {
            return;
        }

        DailyMgr.RegisterDailyAction(shooter, new TriggerShooterDailyAction());
    }

    public MapObject CreatePlantToMap(string resPath, Vector3 logicPos)
    {
        if (!GardenMap.IsCanCreateMapObjectToMap(logicPos))
        {
            return null;
        }

        // 逻辑
        MapObject mapObject = MapObjectMgr.InstanceMapObject();
        GardenMap.AddMapObjectToMap(mapObject.GetAttribute<MapOjectAttribute>().Id, logicPos);
        MapOjectAttribute mapOjectAttribute = mapObject.GetAttribute<MapOjectAttribute>();
        Vector3 postion = new Vector3(logicPos.x * GameDefine.Art.GardenCellSize.x, logicPos.y * GameDefine.Art.GardenCellSize.y, 0);
        mapOjectAttribute.Position = postion;
        mapOjectAttribute.Hp = 5;

        // 表现
        GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(resPath);
        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;
        mapObjectArtAttribute.transform.localPosition = postion;

        RepresentMgr.RegisterMapObject<DeathArtHandle>(mapObject);

        return mapObject;
    }

    public MapObject CreateZombieToMap(string resPath, Vector3 logicPos)
    {
        // 逻辑
        MapObject mapObject = MapObjectMgr.InstanceMapObject();
        MapOjectAttribute mapOjectAttribute = mapObject.GetAttribute<MapOjectAttribute>();
        Vector3 postion = new Vector3(logicPos.x * GameDefine.Art.GardenCellSize.x, logicPos.y * GameDefine.Art.GardenCellSize.y, 0);
        mapOjectAttribute.Position = postion;
        mapOjectAttribute.Hp = 5;

        // 表现
        GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(resPath);
        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;
        mapObjectArtAttribute.transform.localPosition = postion;

        RepresentMgr.RegisterMapObject<MoveArtHandle>(mapObject);
        RepresentMgr.RegisterMapObject<DeathArtHandle>(mapObject);

        DailyMgr.RegisterDailyAction(mapObject, new TriggerZombieMoveDailyAction());

        return mapObject;
    }

}
