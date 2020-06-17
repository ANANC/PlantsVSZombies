using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIController : BaseUIObject
{
    public Transform TopLeftTransform { private set; get; }
    public Transform TopRightTransform { private set; get; }
    public Transform BottomLeftTransform { private set; get; }
    public Transform BottomRightTransform { private set; get; }

    public override void Init()
    {
        TopLeftTransform = FindTransform("TopLeft");
        TopRightTransform = FindTransform("TopRight");
        BottomLeftTransform = FindTransform("BottomLeft");
        BottomRightTransform = FindTransform("BottomRight");
    }

}
