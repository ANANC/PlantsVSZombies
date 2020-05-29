using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IReousrceLoader
{
    void Init();

    void Start();

    void Update();

    void Destroy();

    T LoadResource<T>(string path) where T : Object;
}
