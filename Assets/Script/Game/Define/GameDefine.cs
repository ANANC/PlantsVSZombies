﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDefine 
{
    public const float DeltaTime = 0.02f;

    public static class Scene
    {
        public const string Login = "Login";
        public const string Battle = "Battle";
    }

    public static class Path
    {
        public const string Shooter = "Art/Model/Prefab/Shooter";
        public const string Bullet = "Art/Model/Prefab/Bullet";
        public const string Zombie = "Art/Model/Prefab/Zombie";
        public const string Lawn = "Art/Model/Prefab/Lawn";
        public const string Torchwood = "Art/Model/Prefab/Torchwood";

        public const string BattleSound = "Art/Sound/Prefab/BattleSound";
        public const string LoginSound = "Art/Sound/Prefab/LoginSound";
    }

    public static class Layer
    {
        public const string Plant = "Plant";
        public const string Zombie = "Zombie";
        public const string Wall = "Wall";
        public const string Bullet = "Bullet";
    }

    public static class Garden
    {
        public const int GardenWidth = 5;
        public const int GardenHeight = 4;
    }


    public static class UIName
    {
        public const string MainUI = "MainUI";
        public const string SelectPlantUI = "SelectPlantUI";
        public const string SelectPlantCell = "SelectPlantCell";
        public const string WinPlant = "WinPlant";
        public const string FailPlant = "FailPlant";
        public const string LoginPlant = "LoginPlant";
    }


    public static class Art
    {
        public static Vector3 GardenCellSize = new Vector3(10, 10,0);
    }

    public const int FrameValue = 30;

    public enum PlantType
    {
        Not,
        Shooter
    }

}
 