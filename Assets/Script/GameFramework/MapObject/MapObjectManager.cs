using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectManager:IManager
{
    private List<MapObject> MapObjectList;
    private Dictionary<int, MapObject> MapObjectDict;

    private int AutoId = 1;

    public void Init()
    {
        MapObjectList = new List<MapObject>();
        MapObjectDict = new Dictionary<int, MapObject>();
    }

    public void UnInit()
    {
        DestroyAll();
    }

    public void Start()
    {

    }

    public void Update()
    {

    }

    public MapObject InstanceMapObject()
    {
        MapObject mapObject = CreateMapObject();

        mapObject.GetAttribute<MapOjectAttribute>().Id = AutoId;

        MapObjectList.Add(mapObject);
        MapObjectDict.Add(AutoId, mapObject);

        AutoId += 1;

        return mapObject;
    }

    private MapObject CreateMapObject()
    {
        MapObject mapObject = new MapObject();
        mapObject.Init();

        IAttribute[] attributes = new IAttribute[]
        {
            new MapOjectAttribute(),
            new MapObjectArtAttribute(),
        };

        for (int index = 0; index < attributes.Length; index++)
        {
            IAttribute attribute = attributes[index];
            mapObject.AddAttribute(attribute.GetType().Name, attribute);
        }

        return mapObject;
    }

    public void DeleteMapObject(int id)
    {
        MapObject mapObject;
        if (MapObjectDict.TryGetValue(id, out mapObject))
        {
            mapObject.UnInit();

            GlobalEnvironment.Instance.Get<RepresentManager>().MapObjectUnRegisterAll(mapObject);
            GlobalEnvironment.Instance.Get<DailyManager>().MapObjectUnReigisterAll(mapObject);

            MapObjectDict.Remove(id);
            MapObjectList.Remove(mapObject);
        }
    }

    public MapObject GetMapObject(int id)
    {
        MapObject mapObject;
        if (MapObjectDict.TryGetValue(id, out mapObject))
        {
            return mapObject;
        }
        return null;
    }


    public MapObject GetMapObject(Transform transform)
    {
        MapObject mapObject;
        MapObjectArtAttribute mapObjectArtAttribute;
        for(int index = 0;index < MapObjectList.Count;index++)
        {
            mapObject = MapObjectList[index];
            mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
            if(mapObjectArtAttribute == null || mapObjectArtAttribute.transform == null)
            {
                continue;
            }
            if(mapObjectArtAttribute.transform == transform)
            {
                return mapObject;
            }
        }

        return null;
    }

    public void DestroyAll()
    {
        for (int index = 0;index< MapObjectList.Count;index++)
        {
            MapObject mapObject = MapObjectList[index];

            mapObject.UnInit();

            GlobalEnvironment.Instance.Get<RepresentManager>().MapObjectUnRegisterAll(mapObject);
            GlobalEnvironment.Instance.Get<DailyManager>().MapObjectUnReigisterAll(mapObject);
        }

        MapObjectList.Clear();
        MapObjectDict.Clear();
    }
}
