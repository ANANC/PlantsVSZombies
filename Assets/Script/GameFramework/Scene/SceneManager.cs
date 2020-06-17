using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : IManager
{
    private Dictionary<string,GameScene> SceneDict;
    private GameScene CurScene;


    public void Init()
    {
        SceneDict = new Dictionary<string, GameScene>()
        {
            {GameDefine.Scene.Battle,new BattleGameScene() },
        };

    }

    public void UnInit()
    {

    }

    public void Start()
    {
        Dictionary<string, GameScene>.Enumerator enumerator = SceneDict.GetEnumerator();
        while(enumerator.MoveNext())
        {
            enumerator.Current.Value.Init();
        }
    }


    public void Update()
    {
        if (CurScene != null)
        {
            CurScene.Update();
        }
    }

    public void Enter(string sceneKey)
    {
        if(CurScene != null)
        {
            CurScene.Exist();
        }

        if(SceneDict.TryGetValue(sceneKey,out CurScene))
        {
            CurScene.Enter();
        }
    }
}
