using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBattleGamePlay : GamePlay
{
    private GardenMap gardenMap;
    private MapObjectManager mapObjectManager;

    public override void Enter()
    {
        gardenMap = new GardenMap();
        gardenMap.Enter();

        mapObjectManager = new MapObjectManager();
    }

    public override void Exist()
    {

    }

    public void CreatePlantToMap(int mapObjectType, Vector3 position)
    {
        if (!gardenMap.IsCanCreateMapObjectToMap(position))
        {
            return;
        }

        // 逻辑
        MapObject mapObject = mapObjectManager.InstanceMapObject();
        gardenMap.AddMapObjectToMap(mapObject.GetAttribute<MapOjectAttribute>().Id, position);

        // 表现
        GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance("Art/Module/Prefab/Plant");
        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        mapObjectArtAttribute.gameObject = gameObject;
        mapObjectArtAttribute.transform = gameObject.transform;
    }
}
