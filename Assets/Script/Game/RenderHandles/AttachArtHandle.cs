using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachArtHandle : RepresentHandle
{
    public Color Normal = new Color(0.254f, 0.2862f, 0.2941f, 1);
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
        }

        Color color = Normal;
        if (attachAttackAttribute.Fire > 0)
        {
            color = Red;
        }

        if (mapObjectArtAttribute.material.color != color)
        {
            mapObjectArtAttribute.material.color = color;
        }
        if(!mapObjectArtAttribute.gameObject.activeSelf)
        {
            mapObjectArtAttribute.gameObject.SetActive(true);
        }
    }

}
