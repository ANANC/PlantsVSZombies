using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDefine 
{
    public static class Scene
    {
        public const string Battle = "Battle";
    }

    public static class Path
    {
        public const string Shooter = "Art/Model/Prefab/Shooter";
        public const string Bullet = "Art/Model/Prefab/Bullet";
        public const string Zombie = "Art/Model/Prefab/Zombie";
        public const string Lawn = "Art/Model/Prefab/Lawn";
    }

    public static class Layer
    {
        public const string Plant = "Plant";
        public const string Zombie = "Zombie";
        public const string Wall = "Wall";
    }

    public static class Art
    {
        public static Vector3 GardenCellSize = new Vector3(10, 10,0);
    }

    public const int FrameValue = 30;
}
 