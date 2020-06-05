using UnityEngine;
using UnityEditor;

public class DeathArtHandle : RepresentHandle
{
    public override RepresentManager.ExecuteOrder Order()
    {
        return RepresentManager.ExecuteOrder.Final;
    }


    public override void Execute(MapObject mapObject)
    {
        MapOjectAttribute attribute = mapObject.GetAttribute<MapOjectAttribute>();
        MapObjectArtAttribute art = mapObject.GetAttribute<MapObjectArtAttribute>();

        if (attribute == null || art == null)
        {
            return;
        }

        if (attribute.Hp != 0)
        {
            return;
        }

        GlobalEnvironment.Instance.Get<MapObjectManager>().DeleteMapObject(attribute.Id);
    }
}