using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameScene 
{
    public abstract void Init();
    public abstract void UnInit();

    public abstract void Enter();

    public abstract void Exist();

    public abstract void Update();
}

