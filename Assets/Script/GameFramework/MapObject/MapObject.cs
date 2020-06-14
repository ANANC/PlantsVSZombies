using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject
{
    private List<IAttribute> AttributeList;
    private Dictionary<string, IAttribute> AttributeDict;

    public void Init()
    {
        AttributeList = new List<IAttribute>();
        AttributeDict = new Dictionary<string, IAttribute>();
    }

    public void UnInit()
    {
        for (int index = 0; index < AttributeList.Count; index++)
        {
            AttributeList[index].UnInit();
        }
        AttributeList.Clear();
        AttributeDict.Clear();
    }

    public void AddAttribute<T>(string key, T attribute) where T : IAttribute
    {
        if (AttributeDict.ContainsKey(key))
        {
            return;
        }
        attribute.Init();
        AttributeDict.Add(key, attribute);
        AttributeList.Add(attribute);
    }

    public void RemoveAttibute<T>(string key, T attribute) where T : IAttribute
    {
        if (AttributeDict.ContainsKey(key))
        {
            attribute.UnInit();
            AttributeDict.Remove(key);
            AttributeList.Remove(attribute);
        }
    }
    public T GetAttribute<T>() where T : IAttribute
    {
        string key = typeof(T).Name;
        IAttribute attribute;
        if (AttributeDict.TryGetValue(key, out attribute))
        {
            return (T)attribute;
        }
        return default(T);
    }

    public T GetAttribute<T>(string key) where T : IAttribute
    {
        IAttribute attribute;
        if (AttributeDict.TryGetValue(key, out attribute))
        {
            return (T)attribute;
        }
        return default(T);
    }
}
