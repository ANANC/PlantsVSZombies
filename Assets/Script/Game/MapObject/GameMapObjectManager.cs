using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapObjectManager : IManager
{
    private MapObjectManager MapObjectMgr;
    private RepresentManager RepresentMgr;
    private SkillManager SkillMgr;
    private DailyManager DailyMgr;

    private CellMap GardenMap;

    public void Init()
    {
        SkillMgr = GlobalEnvironment.Instance.Get<SkillManager>();
        MapObjectMgr = GlobalEnvironment.Instance.Get<MapObjectManager>();
        RepresentMgr = GlobalEnvironment.Instance.Get<RepresentManager>();
        DailyMgr = GlobalEnvironment.Instance.Get<DailyManager>();
    }

    public void Start()
    {
    }

    public void UnInit()
    {

    }

    public void Update()
    {

    }

    public void EnterBattle()
    {
        GardenMap = new CellMap();
        GardenMap.Enter();
    }

    public MapObject CreateShooter(Vector3 logicPos)
    {
        MapObject shooter = CreatePlantToMap(GameDefine.Path.Shooter, logicPos);
        if (shooter == null)
        {
            return null;
        }

        DailyMgr.RegisterDailyAction(shooter, new TriggerShooterDailyAction());

        return shooter;
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
        mapOjectAttribute.Position = logicPos;
        mapOjectAttribute.Hp = 5;

        // 表现
        GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(resPath);
        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;
        Vector3 postion = new Vector3(logicPos.x * GameDefine.Art.GardenCellSize.x, logicPos.y * GameDefine.Art.GardenCellSize.y, 0);
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
        mapObjectArtAttribute.MaxSpeed = 0.05f;

        RepresentMgr.RegisterMapObject<MoveArtHandle>(mapObject);
        RepresentMgr.RegisterMapObject<DeathArtHandle>(mapObject);

        DailyMgr.RegisterDailyAction(mapObject, new TriggerZombieMoveDailyAction());

        return mapObject;
    }

    public MapObject CreateBullet(Vector3 position)
    {
        MapObject mapObject = GlobalEnvironment.Instance.Get<MapObjectManager>().InstanceMapObject();

        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(GameDefine.Path.Bullet);
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;
        mapObjectArtAttribute.transform.position = position;
        mapObjectArtAttribute.MaxSpeed = 0.1f;

        MapOjectAttribute mapObjectAttribute = mapObject.GetAttribute<MapOjectAttribute>();
        mapObjectAttribute.Hp = 1;
        mapObjectAttribute.Position = position;

        GlobalEnvironment.Instance.Get<DailyManager>().RegisterDailyAction(mapObject, new BulletMoveDailyAction());

        GlobalEnvironment.Instance.Get<RepresentManager>().RegisterMapObject<MoveArtHandle>(mapObject);
        GlobalEnvironment.Instance.Get<RepresentManager>().RegisterMapObject<DeathArtHandle>(mapObject);

        return mapObject;
    }
}
