﻿using System.Collections;
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
        AutoId += 1;

        MapObjectList.Add(mapObject);
        MapObjectDict.Add(AutoId, mapObject);

        return mapObject;
    }

    private MapObject CreateMapObject()
    {
        MapObject mapObject = new MapObject();
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

    public void RemoveMapObject(int id)
    {
        MapObject mapObject;
        if (MapObjectDict.TryGetValue(id, out mapObject))
        {
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

}
