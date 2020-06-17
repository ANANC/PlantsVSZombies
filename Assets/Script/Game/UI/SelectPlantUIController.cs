using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlantUIController : BaseUIObject
{
    public Transform ContentTransform;
    public RectTransform ContentRectTransform;

    public const int MaxCellCount = 4;

    private List<SelectPlantCellController> Cells;

    public override void Init()
    {
        ContentTransform = FindTransform("Bg/ScrollView/Viewport/Content");
        ContentRectTransform = (RectTransform)ContentTransform;

        CreateCellEnviorment();
    }

    public void CreateCellEnviorment()
    {
        Cells = new List<SelectPlantCellController>(MaxCellCount);
        UIManager uimgr = GlobalEnvironment.Instance.Get<UIManager>();

        for (int index = 0; index < MaxCellCount; index++)
        {
            SelectPlantCellController cell = uimgr.OpenSubUI<SelectPlantCellController>(UIName, GameDefine.UIName.SelectPlantCell, ContentTransform);
            Cells.Add(cell);
        }
    }

    public void RefreshCells(GameDefine.PlantType[] plantTypes)
    {
        int dataCount = plantTypes.Length > MaxCellCount ? MaxCellCount : plantTypes.Length;
        for (int index = 0; index < MaxCellCount; index++)
        {
            if (index < dataCount)
            {
                Cells[index].SetValue(plantTypes[index]);
            }
            else
            {
                Cells[index].Normal();
            }
        }
    }


}
