using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectManager:IManager
{
    private List<MapObject> MapObjectList;
    private Dictionary<int, MapObject> MapObjectDict;

    private Stack<MapObject> PoolStack;
    private int AutoId = 1;

    public void Init()
    {
        MapObjectList = new List<MapObject>();
        MapObjectDict = new Dictionary<int, MapObject>();
        PoolStack = new Stack<MapObject>();
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
        MapObject mapObject;
        if (PoolStack.Count > 0)
        {
            mapObject = PoolStack.Pop();
        }
        else
        {
            mapObject = new MapObject();
        }

        InitMapObject(mapObject);
        mapObject.GetAttribute<MapObjectAttribute>().Id = AutoId;

        MapObjectList.Add(mapObject);
        MapObjectDict.Add(AutoId, mapObject);

        AutoId += 1;

        return mapObject;
    }

    private void InitMapObject(MapObject mapObject)
    {
        mapObject.Init();

        IAttribute[] attributes = new IAttribute[]
        {
            new MapObjectAttribute(),
            new MapObjectArtAttribute(),
        };

        for (int index = 0; index < attributes.Length; index++)
        {
            IAttribute attribute = attributes[index];
            mapObject.AddAttribute(attribute.GetType().Name, attribute);
        }
    }

    public void DeleteMapObject(int id)
    {
        MapObject mapObject;
        if (MapObjectDict.TryGetValue(id, out mapObject))
        {
            UnInitMapObjcet(mapObject);
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
            UnInitMapObjcet(MapObjectList[index]);
        }

        MapObjectList.Clear();
        MapObjectDict.Clear();
    }

    private void UnInitMapObjcet(MapObject mapObject)
    {
        int id = mapObject.GetAttribute<MapObjectAttribute>().Id;

        mapObject.UnInit();

        GlobalEnvironment.Instance.Get<SkillManager>().ClearSkill(mapObject);
        GlobalEnvironment.Instance.Get<BuffManager>().ClaerBuff(mapObject);
        GlobalEnvironment.Instance.Get<RepresentManager>().MapObjectUnRegisterAll(mapObject);
        GlobalEnvironment.Instance.Get<DailyManager>().MapObjectUnReigisterAll(mapObject);

        MapObjectDict.Remove(id);
        MapObjectList.Remove(mapObject);

        PoolStack.Push(mapObject);
    }
}
