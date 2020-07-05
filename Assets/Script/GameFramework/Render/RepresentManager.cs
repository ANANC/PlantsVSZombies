using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepresentManager : IManager
{
    public enum ExecuteOrder
    {
        Normal,
        Final,
    }


    private Dictionary<string, RepresentHandle> HandleInstanceDict;

    private List<RepresentHandle> ExecuteList;
    private Dictionary<RepresentHandle, List<MapObject>> HandleRegisterDict;
    private Dictionary<MapObject, List<RepresentHandle>> MapObjectRegisterDict;

    private List<RepresentHandle> AddMapObjectList;
    private List<RepresentHandle> DeleteMapObjectList;

    public void Init()
    {
        HandleInstanceDict = new Dictionary<string, RepresentHandle>();
        ExecuteList = new List<RepresentHandle>();
        HandleRegisterDict = new Dictionary<RepresentHandle, List<MapObject>>();
        MapObjectRegisterDict = new Dictionary<MapObject, List<RepresentHandle>>();
        AddMapObjectList = new List<RepresentHandle>();
        DeleteMapObjectList = new List<RepresentHandle>();
    }

    public void UnInit()
    {

    }

    public void Start()
    {

    }


    public void Update()
    {
        if (AddMapObjectList.Count > 0)
        {
            for (int index = 0; index < AddMapObjectList.Count; index++)
            {
                ExecuteList.Add(AddMapObjectList[index]);
            }
            AddMapObjectList.Clear();
            ExecuteList.Sort((left, right) =>
            {
                int leftOrder = (int)left.Order();
                int rightOrder = (int)right.Order();
                return leftOrder.CompareTo(rightOrder);
            });
        }

        if(DeleteMapObjectList.Count >0)
        {
            for (int index = 0; index < DeleteMapObjectList.Count; index++)
            {
                ExecuteList.Remove(DeleteMapObjectList[index]);
            }
            DeleteMapObjectList.Clear();
        }

        if(ExecuteList.Count>0)
        {
            for(int index =0;index< ExecuteList.Count;index++)
            {
                RepresentHandle handle = ExecuteList[index];

                List<MapObject> mapObjects;
                if (!HandleRegisterDict.TryGetValue(handle, out mapObjects))
                {
                    Debug.Log("RepresentManager Execute Fail.Handle Register Is Empty. type:" + handle.GetType().Name);
                    break;
                }

                if(mapObjects.Count == 0)
                {
                    DeleteMapObjectList.Add(handle);
                    continue;
                }

                for(int i = 0;i< mapObjects.Count;i++)
                {
                    handle.Execute(mapObjects[i]);
                }
            }
        }
    }


    public void RegisterMapObject<T>(MapObject mapObject)where T: RepresentHandle,new()
    {
        string typeName = typeof(T).Name;

        RepresentHandle handle;
        if(!HandleInstanceDict.TryGetValue(typeName,out handle))
        {
            handle = new T();
            HandleInstanceDict.Add(typeName, handle);
        }

        List<MapObject> mapObjects;
        if(!HandleRegisterDict.TryGetValue(handle,out mapObjects))
        {
            mapObjects = new List<MapObject>();
            HandleRegisterDict.Add(handle, mapObjects);
        }
        mapObjects.Add(mapObject);

        if (!ExecuteList.Contains(handle))
        {
            AddMapObjectList.Add(handle);
        }

        List<RepresentHandle> handles;
        if(!MapObjectRegisterDict.TryGetValue(mapObject,out handles))
        {
            handles = new List<RepresentHandle>();
            MapObjectRegisterDict.Add(mapObject, handles);
        }
        handles.Add(handle);
    }

    public void UnRegisterMapObject<T>(MapObject mapObject) where T : RepresentHandle
    {
        string typeName = typeof(T).Name;

        RepresentHandle handle;
        if (!HandleInstanceDict.TryGetValue(typeName, out handle))
        {
            return;
        }

        List<MapObject> mapObjects;
        if (HandleRegisterDict.TryGetValue(handle, out mapObjects))
        {
            mapObjects.Remove(mapObject);
            if (mapObjects.Count == 0)
            {
                DeleteMapObjectList.Add(handle);
            }
        }

        List<RepresentHandle> handles;
        if (MapObjectRegisterDict.TryGetValue(mapObject, out handles))
        {
            handles.Remove(handle);
            if(handles.Count == 0)
            {
                MapObjectRegisterDict.Remove(mapObject);
            }
        }
    }

    public void MapObjectUnRegisterAll(MapObject mapObject)
    {
        List<RepresentHandle> handles;
        if (MapObjectRegisterDict.TryGetValue(mapObject, out handles))
        {
            for(int index = 0;index<handles.Count;index++)
            {
                RepresentHandle handle = handles[index];

                List<MapObject> mapObjects;
                if (HandleRegisterDict.TryGetValue(handle, out mapObjects))
                {
                    mapObjects.Remove(mapObject);
                    if (mapObjects.Count == 0)
                    {
                        DeleteMapObjectList.Add(handle);
                    }
                }
            }

            MapObjectRegisterDict.Remove(mapObject);
        }
    }



}
