using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyManager : IManager
{

    private List<DailyAction> AddList;
    private List<DailyAction> DeleteList;
    private List<DailyAction> RegisterList;

    public void Init()
    {
        RegisterList = new List<DailyAction>();
        AddList = new List<DailyAction>();
        DeleteList = new List<DailyAction>();
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
                RegisterList.Add(AddList[index]);
            }
            AddList.Clear();
        }

        if (DeleteList.Count > 0)
        {
            for (int index = 0; index < DeleteList.Count; index++)
            {
                RegisterList.Remove(DeleteList[index]);
            }
            DeleteList.Clear();
        }

        if (RegisterList.Count > 0)
        {
            for (int index = 0; index < RegisterList.Count; index++)
            {
                DailyAction dailyAction = RegisterList[index];
                bool complete = dailyAction.Complete();
                if (complete)
                {
                    DeleteList.Add(dailyAction);
                }
            }
        }
    }

    public void RegisterDailyAction<T>(T dailyAction) where T : DailyAction
    {
        AddList.Add(dailyAction);
    }

}
