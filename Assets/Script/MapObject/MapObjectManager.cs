using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectManager
{
    private List<MapObject> MapObjectList = new List<MapObject>();
    private Dictionary<int, MapObject> MapObjectDict = new Dictionary<int, MapObject>();

    private int m_Auto = 1;

    public MapObject AddMapObject()
    {
        MapObject mapObject = CreateMapObject();
        mapObject.GetAttribute<MapOjectAttribute>().Id = m_Auto;
        m_Auto += 1;

        MapObjectList.Add(mapObject);
        MapObjectDict.Add(m_Auto, mapObject);

        return mapObject;
    }

    public MapObject CreateMapObject()
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
