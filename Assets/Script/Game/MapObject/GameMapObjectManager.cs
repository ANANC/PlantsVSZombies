using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapObjectManager : IManager
{
    private GameObject MapObjctParent;

    private MapObjectManager MapObjectMgr;
    private RepresentManager RepresentMgr;
    private DailyManager DailyMgr;

    private CellMap GardenMap;

    private List<MapObject> AllMapObjectList;

    private Dictionary<int, string> MapObjectResPathDict;
    private Dictionary<string, Stack<GameObject>> ResourceGameObjectPool;


    public void Init()
    {
        MapObjectMgr = GlobalEnvironment.Instance.Get<MapObjectManager>();
        RepresentMgr = GlobalEnvironment.Instance.Get<RepresentManager>();
        DailyMgr = GlobalEnvironment.Instance.Get<DailyManager>();
        ResourceGameObjectPool = new Dictionary<string, Stack<GameObject>>();
        MapObjectResPathDict = new Dictionary<int, string>();
        AllMapObjectList = new List<MapObject>();


        MapObjctParent = new GameObject("MapObjct");
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

    public void ExistBattle()
    {
        GardenMap.Exist();
        GardenMap = null;

        DestroyAll();
        MapObjectMgr.DestroyAll();

        MapObjectResPathDict.Clear();
        AllMapObjectList.Clear();

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

    public MapObject CreateTorchwood(Vector3 logicPos)
    {
        MapObject torchwood = CreatePlantToMap(GameDefine.Path.Torchwood, logicPos);
        if (torchwood == null)
        {
            return null;
        }

        DailyMgr.RegisterDailyAction(torchwood, new TorchwoodDailyAction());

        return torchwood;
    }

    public MapObject CreatePlantToMap(string resPath, Vector3 logicPos)
    {
        if (!GardenMap.IsCanCreateMapObjectToMap(logicPos))
        {
            return null;
        }

        // 逻辑
        MapObject mapObject = MapObjectMgr.InstanceMapObject();
        GardenMap.AddMapObjectToMap(mapObject.GetAttribute<MapObjectAttribute>().Id, logicPos);
        MapObjectAttribute mapObjectAttribute = mapObject.GetAttribute<MapObjectAttribute>();
        mapObjectAttribute.Position = logicPos;
        mapObjectAttribute.Hp = 5;

        // 表现    
        GameObject gameObject = PopPool(resPath,true);
        if (gameObject == null)
        {
            gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(resPath);
            gameObject.transform.SetParent(MapObjctParent.transform);
        }
        MapObjectResPathDict.Add(mapObjectAttribute.Id, resPath);
        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;
        Vector3 postion = new Vector3(logicPos.x * GameDefine.Art.GardenCellSize.x, logicPos.y * GameDefine.Art.GardenCellSize.y, 0);
        mapObjectArtAttribute.transform.position = postion;

        RepresentMgr.RegisterMapObject<DeathArtHandle>(mapObject);

        AllMapObjectList.Add(mapObject);

        return mapObject;
    }

    public MapObject CreateZombieToMap(string resPath, Vector3 logicPos)
    {
        // 逻辑
        MapObject mapObject = MapObjectMgr.InstanceMapObject();
        MapObjectAttribute mapObjectAttribute = mapObject.GetAttribute<MapObjectAttribute>();
        Vector3 postion = new Vector3(logicPos.x * GameDefine.Art.GardenCellSize.x, logicPos.y * GameDefine.Art.GardenCellSize.y, 0);
        mapObjectAttribute.Position = postion;
        mapObjectAttribute.Hp = 5;

        // 表现
        GameObject gameObject = PopPool(resPath,true);
        if (gameObject == null)
        {
            gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(resPath);
            gameObject.transform.SetParent(MapObjctParent.transform);
        }
        MapObjectResPathDict.Add(mapObjectAttribute.Id, resPath);
        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;
        mapObjectArtAttribute.transform.position = postion;
        mapObjectArtAttribute.MaxSpeed = 0.001f;

        RepresentMgr.RegisterMapObject<MoveArtHandle>(mapObject);
        RepresentMgr.RegisterMapObject<DeathArtHandle>(mapObject);

        DailyMgr.RegisterDailyAction(mapObject, new TriggerZombieMoveDailyAction());

        AllMapObjectList.Add(mapObject);

        return mapObject;
    }

    public MapObject CreateBullet(Vector3 position)
    {
        string resPath = GameDefine.Path.Bullet;

        MapObject mapObject = GlobalEnvironment.Instance.Get<MapObjectManager>().InstanceMapObject();

        MapObjectAttribute mapObjectAttribute = mapObject.GetAttribute<MapObjectAttribute>();
        mapObjectAttribute.Hp = 1;
        mapObjectAttribute.Position = position;

        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        GameObject gameObject = PopPool(resPath,false);
        if (gameObject == null)
        {
            gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(resPath);
            gameObject.transform.SetParent(MapObjctParent.transform);
        }
        MapObjectResPathDict.Add(mapObjectAttribute.Id, resPath);
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;
        mapObjectArtAttribute.transform.position = position;
        mapObjectArtAttribute.MaxSpeed = 0.002f;

        mapObject.AddAttribute<AttachAttackAttribute>(typeof(AttachAttackAttribute).Name, new AttachAttackAttribute());

        GlobalEnvironment.Instance.Get<DailyManager>().RegisterDailyAction(mapObject, new BulletMoveDailyAction());

        GlobalEnvironment.Instance.Get<RepresentManager>().RegisterMapObject<MoveArtHandle>(mapObject);
        GlobalEnvironment.Instance.Get<RepresentManager>().RegisterMapObject<DeathArtHandle>(mapObject); 
        GlobalEnvironment.Instance.Get<RepresentManager>().RegisterMapObject<AttachArtHandle>(mapObject);

        AllMapObjectList.Add(mapObject);

        return mapObject;
    }

    public void DestroyMapObject(MapObject mapObject)
    {
        int id = mapObject.GetAttribute<MapObjectAttribute>().Id;

        string resPath;
        if(MapObjectResPathDict.TryGetValue(id, out resPath))
        {
            MapObjectResPathDict.Remove(id);
            MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
            PushPool(resPath, mapObjectArtAttribute.gameObject);
        }
        else
        {
            Debug.LogError("MapObjectResPathDict Not Recoed! id:" + id);
        }

        MapObjectMgr.DeleteMapObject(id);
        AllMapObjectList.Remove(mapObject);
    }

    public void DestroyAll()
    {
        while (AllMapObjectList.Count>0)
        {
            DestroyMapObject(AllMapObjectList[0]);
        }
    }

    private void PushPool(string resPath,GameObject gameObject)
    {
        gameObject.SetActive(false);
        if (!ResourceGameObjectPool.ContainsKey(resPath))
        {
            ResourceGameObjectPool.Add(resPath, new Stack<GameObject>());
        }
        ResourceGameObjectPool[resPath].Push(gameObject);
    }
    private GameObject PopPool(string resPath,bool active)
    {
        Stack<GameObject> gameObjects;
        if(ResourceGameObjectPool.TryGetValue(resPath,out gameObjects))
        {
            if(gameObjects.Count == 0)
            {
                return null;
            }
            GameObject gameObject = gameObjects.Pop();
            gameObject.SetActive(active);
            return gameObject;
        }
        return null;
    }
}
