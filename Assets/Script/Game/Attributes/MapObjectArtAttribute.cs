using UnityEngine;
using UnityEditor;


public class MapObjectArtAttribute : IAttribute
{
    public GameObject gameObject;
    public Transform transform;
    public Material material;
    public float MaxSpeed;

    public void Init()
    {

    }

    public void UnInit()
    {
        gameObject = null;
        transform = null;
        MaxSpeed = 0;
    }
}

