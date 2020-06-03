﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    void Start()
    {
        FormworkRegister();
        GameRegister();

        StartGame();
    }

    void Update()
    {
        GlobalEnvironment.Instance.Update();
    }

    private void GameRegister()
    {
        GlobalEnvironment.Instance.AddManager<MapObjectManager>(new MapObjectManager());
        GlobalEnvironment.Instance.AddManager<DailyManager>(new DailyManager());
        GlobalEnvironment.Instance.AddManager<SceneManager>(new SceneManager());
    }

    private void FormworkRegister()
    {
        GlobalEnvironment.Instance.AddManager<ResourceManager>(new ResourceManager());
    }

    private void StartGame()
    {

    }
}
