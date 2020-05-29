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
        MapObject mapObject = mapObjectManager.CreateMapObject();
        gardenMap.AddMapObjectToMap(mapObject.GetAttribute<MapOjectAttribute>().Id, position);
        //创建资源
    }
}
