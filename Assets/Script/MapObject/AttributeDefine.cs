using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttribute
{ }

public class MapOjectAttribute : IAttribute
{
    public int Id;
    public Vector3 Position;
    public int Hp;
}

public class MapObjectArtAttribute : IAttribute
{
    public GameObject gameObject;
    public Transform transform;
}

