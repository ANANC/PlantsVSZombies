using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachArtHandle : RepresentHandle
{
    public Color Red = new Color(0.735f, 0.1f, 0.1f, 1);

    public override RepresentManager.ExecuteOrder Order()
    {
        return RepresentManager.ExecuteOrder.Normal;
    }

    public override void Execute(MapObject mapObject)
    {
        AttachAttackAttribute attachAttackAttribute = mapObject.GetAttribute<AttachAttackAttribute>();
        if (attachAttackAttribute == null)
        {
            return;
        }
        MapObjectArtAttribute mapObjectArtAttribute = mapObject.GetAttribute<MapObjectArtAttribute>();
        if(mapObjectArtAttribute == null)
        {
            return;
        }
        Material material = mapObjectArtAttribute.material;
        if(material == null)
        {
            material = mapObjectArtAttribute.transform.GetComponent<MeshRenderer>().material;
            mapObjectArtAttribute.material = material;
            mapObjectArtAttribute.normalColor = material.color;
        }

        Color color = mapObjectArtAttribute.normalColor;
        if (attachAttackAttribute.Fire > 0)
        {
            color = Red;
        }

        mapObjectArtAttribute.material.color = color;
    }

}
