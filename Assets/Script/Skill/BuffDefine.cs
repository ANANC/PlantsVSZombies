using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff { }


public class HpControl : IBuff
{
    public static void BuckleUp(MapOjectAttribute attribute, int value)
    {
        attribute.Hp -= value;
        if (attribute.Hp < 0)
        {
            attribute.Hp = 0;
        }
    }
}
