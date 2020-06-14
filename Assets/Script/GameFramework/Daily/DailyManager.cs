using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyManager : IManager
{
    private List<DailyAction> AddList;
    private List<DailyAction> DeleteList;
    private List<DailyAction> ExecuteList;

    private Dictionary<MapObject, List<DailyAction>> MapObjectRegisterDict;

    public void Init()
    {
        ExecuteList = new List<DailyAction>();
        AddList = new List<DailyAction>();
        DeleteList = new List<DailyAction>();
        MapObjectRegisterDict = new Dictionary<MapObject, List<DailyAction>>();
    }

    public void UnInit()
    {

    }

    public void Start()
    {

    }


    public void Update()
    {
        if (AddList.Count > 0)
        {
            for (int index = 0; index < AddList.Count; index++)
            {
                AddList[index].Init();
                ExecuteList.Add(AddList[index]);
            }
            AddList.Clear();
        }

        if (DeleteList.Count > 0)
        {
            for (int index = 0; index < DeleteList.Count; index++)
            {
                ExecuteList.Remove(DeleteList[index]);
            }
            DeleteList.Clear();
        }

        if (ExecuteList.Count > 0)
        {
            for (int index = 0; index < ExecuteList.Count; index++)
            {
                DailyAction dailyAction = ExecuteList[index];
                bool complete = dailyAction.Complete();
                if (complete)
                {
                    DeleteList.Add(dailyAction);
                }
            }
        }
    }

    public void RegisterDailyAction(MapObject mapObject, DailyAction dailyAction)
    {
        if (mapObject == null)
        {
            return;
        }

        dailyAction.mapObject = mapObject;

        AddList.Add(dailyAction);

        List<DailyAction> dailyActions;
        if (!MapObjectRegisterDict.TryGetValue(mapObject, out dailyActions))
        {
            dailyActions = new List<DailyAction>();
            MapObjectRegisterDict.Add(mapObject, dailyActions);
        }
        dailyActions.Add(dailyAction);
    }

    public void MapObjectUnReigisterAll(MapObject mapObject)
    {
        List<DailyAction> dailyActions;
        if (!MapObjectRegisterDict.TryGetValue(mapObject, out dailyActions))
        {
            return;
        }
        MapObjectRegisterDict.Remove(mapObject);

        for (int index = 0; index < dailyActions.Count; index++)
        {
            DeleteList.Add(dailyActions[index]);
        }
    }
}
