using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOjectAttribute : IAttribute
{
    public int Id;
    public Vector3 Position;
    public int Hp;

    public void Init()
    {
        Id = 0;
        Position = Vector3.zero;
        Hp = 5;
    }

    public void UnInit()
    {

    }
}
