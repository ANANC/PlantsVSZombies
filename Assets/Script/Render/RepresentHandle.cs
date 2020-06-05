using UnityEngine;
using UnityEditor;

public abstract class RepresentHandle
{
    public abstract RepresentManager.ExecuteOrder Order();
    public abstract void Execute(MapObject mapObject);
}