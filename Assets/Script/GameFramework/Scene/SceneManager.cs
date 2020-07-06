using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : IManager
{
    private Dictionary<string,GameScene> SceneDict;
    private GameScene CurScene;
    private GameScene ChangeScene;


    public void Init()
    {
        SceneDict = new Dictionary<string, GameScene>()
        {
            { GameDefine.Scene.Login,new LoginGameScene() },
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
        if (ChangeScene != null)
        {
            CurScene = ChangeScene;
            ChangeScene = null;
            CurScene.Enter();
        }

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

        SceneDict.TryGetValue(sceneKey, out ChangeScene);
    }

    public GameScene GetScene(string sceneType)
    {
        GameScene scene;
        if (SceneDict.TryGetValue(sceneType, out scene))
        {
            return scene;
        }
        return null;
    }
}
