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

    private Dictionary<RepresentHandle, List<MapObject>> AddMapObjectDict;
    private Dictionary<RepresentHandle, List<MapObject>> DeleteMapObjectDict;

    public void Init()
    {
        HandleInstanceDict = new Dictionary<string, RepresentHandle>();
        ExecuteList = new List<RepresentHandle>();
        HandleRegisterDict = new Dictionary<RepresentHandle, List<MapObject>>();
        MapObjectRegisterDict = new Dictionary<MapObject, List<RepresentHandle>>();
        AddMapObjectDict = new Dictionary<RepresentHandle, List<MapObject>>();
        DeleteMapObjectDict = new Dictionary<RepresentHandle, List<MapObject>>();
    }

    public void UnInit()
    {

    }

    public void Start()
    {

    }


    public void Update()
    {
        if (AddMapObjectDict.Count > 0)
        {
            Dictionary<RepresentHandle, List<MapObject>>.Enumerator enumerator = AddMapObjectDict.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!ExecuteList.Contains(enumerator.Current.Key))
                {
                    ExecuteList.Add(enumerator.Current.Key);
                }
                List<MapObject> mapObjects;
                if (!HandleRegisterDict.TryGetValue(enumerator.Current.Key, out mapObjects))
                {
                    mapObjects = new List<MapObject>();
                    HandleRegisterDict.Add(enumerator.Current.Key, mapObjects);
                }
                for (int index = 0; index < enumerator.Current.Value.Count; index++)
                {
                    mapObjects.Add(enumerator.Current.Value[index]);
                }
            }
            AddMapObjectDict.Clear();
            ExecuteList.Sort((left, right) =>
            {
                int leftOrder = (int)left.Order();
                int rightOrder = (int)right.Order();
                return leftOrder.CompareTo(rightOrder);
            });
        }

        if (DeleteMapObjectDict.Count > 0)
        {
            Dictionary<RepresentHandle, List<MapObject>>.Enumerator enumerator = DeleteMapObjectDict.GetEnumerator();
            while (enumerator.MoveNext())
            {
                List<MapObject> mapObjects;
                if (HandleRegisterDict.TryGetValue(enumerator.Current.Key, out mapObjects))
                {
                    for (int index = 0; index < enumerator.Current.Value.Count; index++)
                    {
                        mapObjects.Remove(enumerator.Current.Value[index]);
                    }

                    if (mapObjects.Count == 0)
                    {
                        ExecuteList.Remove(enumerator.Current.Key);
                    }
                }
                else
                {
                    Debug.LogError("RepresentManager Delete Fail.Handle Type Is Empty. type:" + enumerator.Current.Key.GetType().Name);
                }
            }
            DeleteMapObjectDict.Clear();
        }

        if (ExecuteList.Count > 0)
        {
            for (int index = 0; index < ExecuteList.Count; index++)
            {
                RepresentHandle handle = ExecuteList[index];

                List<MapObject> mapObjects;
                if (!HandleRegisterDict.TryGetValue(handle, out mapObjects))
                {
                    Debug.LogError("RepresentManager Execute Fail.Handle Register Is Empty. type:" + handle.GetType().Name);
                    continue;
                }

                if (mapObjects.Count != 0)
                {
                    for (int i = 0; i < mapObjects.Count; i++)
                    {
                        handle.Execute(mapObjects[i]);
                    }
                }
                else
                {
                    Debug.LogError("RepresentManager Execute count is Empty. type:" + handle.GetType().Name);
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

        if(!AddMapObjectDict.ContainsKey(handle))
        {
            AddMapObjectDict.Add(handle, new List<MapObject>());
        }
        AddMapObjectDict[handle].Add(mapObject);

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

        if (!DeleteMapObjectDict.ContainsKey(handle))
        {
            DeleteMapObjectDict.Add(handle, new List<MapObject>());
        }
        DeleteMapObjectDict[handle].Add(mapObject);

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
                if (!DeleteMapObjectDict.ContainsKey(handle))
                {
                    DeleteMapObjectDict.Add(handle, new List<MapObject>());
                }
                DeleteMapObjectDict[handle].Add(mapObject);
            }

            MapObjectRegisterDict.Remove(mapObject);
        }
    }



}
