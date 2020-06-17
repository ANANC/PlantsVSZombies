using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectPlantCellController : BaseUIObject
{
    private Text TextText;
    private GameDefine.PlantType PlantType;
    private EventTriggerListener ImageEventTriggerListener;

    public override void Init()
    {
        TextText = FindComponent<Text>("Image/Text");
        ImageEventTriggerListener = FindComponent<EventTriggerListener>("Image");
        ImageEventTriggerListener.onBeginDrag = ImageOnBeginDrag;
    }

    public void Normal()
    {
        PlantType = GameDefine.PlantType.Not;
        TextText.text = "空";
    }

    public void SetValue(GameDefine.PlantType type)
    {
        PlantType = type;
        TextText.text = Enum.GetName(typeof(GameDefine.PlantType), PlantType);
    }

    public void ImageOnBeginDrag(GameObject go, PointerEventData eventData)
    {

    }
}
