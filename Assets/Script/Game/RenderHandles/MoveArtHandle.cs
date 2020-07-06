using UnityEngine;
using UnityEditor;

public class MoveArtHandle : RepresentHandle
{
    public override RepresentManager.ExecuteOrder Order()
    {
        return RepresentManager.ExecuteOrder.Normal;
    }

    public override void Execute(MapObject mapObject)
    {
        MapObjectAttribute attribute = mapObject.GetAttribute<MapObjectAttribute>();
        MapObjectArtAttribute art = mapObject.GetAttribute<MapObjectArtAttribute>();

        if (attribute == null || art == null)
        {
            return;
        }

        if (art.transform == null)
        {
            return;
        }

        if(art.MaxSpeed == 0)
        {
            Debug.LogError("MaxSpeed Is 0. mapObject Name:" + art.transform.name);
        }

        Vector3 transformPos = art.transform.position;
        Vector3 distance = attribute.Position - transformPos;

        if (distance.x > art.MaxSpeed || distance.x < -art.MaxSpeed)
        {
            distance.x = art.MaxSpeed * (distance.x > 0 ? 1 : -1);
        }

        if (distance.y > art.MaxSpeed && distance.y < -art.MaxSpeed)
        {
            distance.y = art.MaxSpeed * (distance.y > 0 ? 1 : -1);
        }

        art.transform.position += distance;
    }
}