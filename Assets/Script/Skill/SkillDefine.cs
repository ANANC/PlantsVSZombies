using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISkill { }

public class NormalAttack : ISkill
{
    private static int value = 1;
    public static void Execute(MapObject mapObject)
    {
        HpControl.BuckleUp(mapObject.GetAttribute<MapOjectAttribute>(), value);
    }
}