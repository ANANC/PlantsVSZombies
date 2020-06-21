using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenUIController : BaseUIObject
{
    private RectTransform Image;

    private List<RectTransform> ImageList;
    public override void Init()
    {
        Image = FindComponent<RectTransform>("Image");


    }
}
