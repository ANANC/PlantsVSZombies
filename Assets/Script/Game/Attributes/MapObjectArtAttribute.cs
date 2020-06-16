using UnityEngine;
using UnityEditor;


public class MapObjectArtAttribute : IAttribute
{
    public GameObject gameObject;
    public Transform transform;
    public float MaxSpeed;

    public void Init()
    {

    }

    public void UnInit()
    {
        if(gameObject != null)
        {
            GameObject.Destroy(gameObject);
        }
    }
}

