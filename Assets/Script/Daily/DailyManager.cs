using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyManager:IManager
{
    Dictionary<string, DailyAction> DailyTypeDict;

    Dictionary<DailyAction, List<MapObject>> AddDict;
    Dictionary<DailyAction, List<MapObject>> DeleteDict;
    Dictionary<DailyAction, List<MapObject>> RegisterDict;

    public void Init()
    {
        DailyTypeDict = new Dictionary<string, DailyAction>();
        RegisterDict = new Dictionary<DailyAction, List<MapObject>>();
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

    public void RegisterDailyAction<T>(MapObject mapObject) where T : DailyAction,new()
    {
        DailyAction dailyAction;
        List<MapObject> mapObjects;

        string typeName = typeof(T).Name;

        if (!DailyTypeDict.TryGetValue(typeName, out dailyAction))
        {
            dailyAction = new T();
            DailyTypeDict.Add(typeName, dailyAction);
        }
        if(!AddDict.TryGetValue(dailyAction,out mapObjects))
        {
            mapObjects = new List<MapObject>();
            AddDict.Add(dailyAction, mapObjects);
        }

        mapObjects.Add(mapObject);
    }

}
