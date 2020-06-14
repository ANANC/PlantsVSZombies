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
        MapOjectAttribute attribute = mapObject.GetAttribute<MapOjectAttribute>();
        MapObjectArtAttribute art = mapObject.GetAttribute<MapObjectArtAttribute>();

        if (attribute == null || art == null)
        {
            return;
        }

        if (art.transform == null)
        {
            return;
        }

        Vector3 transformPos = art.transform.position;
        Vector3 distance = attribute.Position - transformPos;

        float speed = 0.1f;

        if (distance.x > 0.01 || distance.x < -0.01)
        {
            distance.x = speed * Time.deltaTime * (distance.x > 0 ? 1 : -1);
        }
        if (distance.y > 0.01 && distance.y < -0.01)
        {
            distance.y = speed * Time.deltaTime * (distance.y > 0 ? 1 : -1);
        }

        art.transform.position += distance;
    }
}