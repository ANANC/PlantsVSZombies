using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBattleGamePlay : GamePlay
{
    private GardenMap gardenMap;
    private MapObjectManager mapObjectManager;
    private RepresentManager representManager;

    
    public override void Enter()
    {
        mapObjectManager = GlobalEnvironment.Instance.Get<MapObjectManager>();
        representManager = GlobalEnvironment.Instance.Get<RepresentManager>();

        gardenMap = new GardenMap();
        gardenMap.Enter();

    }

    public override void Exist()
    {

    }

    public MapObject CreatePlantToMap(string resPath, Vector3 logicPos)
    {
        if (!gardenMap.IsCanCreateMapObjectToMap(logicPos))
        {
            return null;
        }

        // 逻辑
        MapObject mapObject = mapObjectManager.InstanceMapObject();
        gardenMap.AddMapObjectToMap(mapObject.GetAttribute<MapOjectAttribute>().Id, logicPos);
        MapOjectAttribute mapOjectAttribute = mapObject.GetAttribute<MapOjectAttribute>();
        Vector3 postion = new Vector3(logicPos.x * GameDefine.Art.GardenCellSize.x, logicPos.y * GameDefine.Art.GardenCellSize.y, 0);
        mapOjectAttribute.Position = postion;

        // 表现
        GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(resPath);
        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;
        representManager.RegisterMapObject(mapObject);

        return mapObject;
    }
}
