using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : IManager
{
    private List<Buff> ExecuteBuffList;
    private List<Buff> AddBuffList;
    private List<Buff> DeleteBuffList;

    public void Init()
    {
        ExecuteBuffList = new List<Buff>();
        AddBuffList = new List<Buff>();
        DeleteBuffList = new List<Buff>();
    }

    public void UnInit()
    {
    }

    public void Start()
    {
    }



    public void Update()
    {
        if (AddBuffList.Count > 0)
        {
            for (int index = 0; index < AddBuffList.Count; index++)
            {
                AddBuffList[index].Init();
                ExecuteBuffList.Add(AddBuffList[index]);
            }
            AddBuffList.Clear();
        }

        if (DeleteBuffList.Count > 0)
        {
            for (int index = 0; index < DeleteBuffList.Count; index++)
            {
                ExecuteBuffList.Remove(DeleteBuffList[index]);
            }
            DeleteBuffList.Clear();
        }

        for (int index = 0; index < ExecuteBuffList.Count; index++)
        {
            Buff Buff = ExecuteBuffList[index];
            if (Buff.Complete())
            {
                DeleteBuffList.Add(Buff);
            }
        }
    }

    public void AddBuff(MapObject mapObject, Buff Buff)
    {
        Buff.mapObject = mapObject;
        AddBuffList.Add(Buff);
    }
}
