using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectPlantCellController : BaseUIObject
{
    private RectTransform RootRectTransform;
    private Text TextText;
    private GameDefine.PlantType PlantType;
    private Image ImageImage;
    private EventTriggerListener ImageEventTriggerListener;
    private SelectPlantCellController DrapCellController;
    private Vector3 Offset;

    public override void Init()
    {
        RootRectTransform = (RectTransform)Transform;
        TextText = FindComponent<Text>("Image/Text");
        ImageImage = FindComponent<Image>("Image");
        ImageEventTriggerListener = FindComponent<EventTriggerListener>("Image");
        ImageEventTriggerListener.onBeginDrag = ImageOnBeginDrag;
        ImageEventTriggerListener.onDrag = ImageOnDrag;
        ImageEventTriggerListener.onEndDrag = ImageOnEndDrag;

    }

    public void Normal()
    {
        PlantType = GameDefine.PlantType.Not;
        TextText.text = "空";
        ImageImage.raycastTarget = false;
    }

    public void SetValue(GameDefine.PlantType type)
    {
        PlantType = type;
        TextText.text = Enum.GetName(typeof(GameDefine.PlantType), PlantType);
        ImageImage.raycastTarget = true;
    }

    public void ImageOnBeginDrag(GameObject go, PointerEventData eventData)
    {
        DrapCellController = GlobalEnvironment.Instance.Get<UIManager>().OpenUI<SelectPlantCellController>(GameDefine.UIName.SelectPlantCell);
        DrapCellController.ImageImage.raycastTarget = false;
        DrapCellController.Transform.position = Transform.position;
    }

    public void ImageOnDrag(GameObject go, PointerEventData eventData)
    {
        Vector3 globalMousePos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(DrapCellController.RootRectTransform, eventData.position, null, out globalMousePos))
        {
            DrapCellController.Transform.position =  globalMousePos;
        }
    }

    public void ImageOnEndDrag(GameObject go, PointerEventData eventData)
    {
        GlobalEnvironment.Instance.Get<UIManager>().DestroyUI(DrapCellController.UIName);
        DrapCellController = null;
    }
}
