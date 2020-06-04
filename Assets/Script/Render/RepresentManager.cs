using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepresentManager : IManager
{
    private List<MapObject> MapObjectList;

    private List<MapObject> AddMapObjectList;
    private List<MapObject> DeleteMapObjectList;

    public void Init()
    {
        MapObjectList = new List<MapObject>();
        AddMapObjectList = new List<MapObject>();
        DeleteMapObjectList = new List<MapObject>();
    }
    public void UnInit()
    {

    }

    public void Start()
    {

    }


    public void Update()
    {
        if(AddMapObjectList.Count>0)
        {
            for(int index = 0;index< AddMapObjectList.Count;index++)
            {
                MapObjectList.Add(AddMapObjectList[index]);
            }
        }

        if(DeleteMapObjectList.Count >0)
        {
            for (int index = 0; index < DeleteMapObjectList.Count; index++)
            {
                MapObjectList.Remove(DeleteMapObjectList[index]);
            }
        }

        if(MapObjectList.Count>0)
        {
            for(int index =0;index< MapObjectList.Count;index++)
            {
                MapObject mapObject = MapObjectList[index];

                Move(mapObject);
                //Death(mapObject);
            }
        }
    }


    public void RegisterMapObject(MapObject mapObject)
    {
        AddMapObjectList.Add(mapObject);
    }


    private void Move(MapObject mapObject)
    {
        MapOjectAttribute attribute = mapObject.GetAttribute<MapOjectAttribute>();
        MapObjectArtAttribute art = mapObject.GetAttribute<MapObjectArtAttribute>();

        if (attribute == null || art == null)
        {
            return;
        }

        if(art.transform == null)
        {
            return;
        }

        Vector3 transformPos = art.transform.position;
        Vector3 distance = attribute.Position - transformPos;

        float speed = 0.001f;

        if (distance.x > 0.01 || distance.x< -0.01)
        {
            distance.x = speed * Time.deltaTime * (distance.x > 0 ? 1 : -1);
        }
        if (distance.y > 0.01 && distance.y < -0.01)
        {
            distance.y = speed * Time.deltaTime * (distance.y > 0 ? 1 : -1);
        }

        art.transform.position += distance;
    }

    private void Death(MapObject mapObject)
    {
        MapOjectAttribute attribute = mapObject.GetAttribute<MapOjectAttribute>();
        MapObjectArtAttribute art = mapObject.GetAttribute<MapObjectArtAttribute>();

        if (attribute == null || art == null)
        {
            return;
        }

        if(attribute.Hp != 0)
        {
            return;
        }

        GlobalEnvironment.Instance.Get<MapObjectManager>().RemoveMapObject(attribute.Id);
        DeleteMapObjectList.Add(mapObject);
    }
}
