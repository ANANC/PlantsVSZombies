using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    void Start()
    {
        FormworkRegister();
        GameRegister();
    }

    void Update()
    {
        GlobalEnvironment.Instance.Update();
    }

    private void GameRegister()
    {
        GlobalEnvironment.Instance.AddManager<MapObjectManager>(new MapObjectManager());

    }


    private void FormworkRegister()
    {
        GlobalEnvironment.Instance.AddManager<ResourceManager>(new ResourceManager());
    }
}
