using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    void Start()
    {
        FormworkRegister();
        GameRegister();

        GlobalEnvironment.Instance.Start();

       StartGame();
    }

    void Update()
    {
        GlobalEnvironment.Instance.Update();
    }

    private void GameRegister()
    {
        GlobalEnvironment.Instance.AddManager(new MapObjectManager());
        GlobalEnvironment.Instance.AddManager(new DailyManager());
        GlobalEnvironment.Instance.AddManager(new SceneManager());
        GlobalEnvironment.Instance.AddManager(new RepresentManager());
        GlobalEnvironment.Instance.AddManager(new SkillManager());
        GlobalEnvironment.Instance.AddManager(new GameMapObjectManager());
        GlobalEnvironment.Instance.AddManager(new BuffManager());
    }

    private void FormworkRegister()
    {
        GlobalEnvironment.Instance.AddManager(new UIManager());
        GlobalEnvironment.Instance.AddManager(new ResourceManager());
    }

    private void StartGame()
    {
        UIManager uiMgr = GlobalEnvironment.Instance.Get<UIManager>();
        uiMgr.SetUIFolderPath("Art/UI/Prefab");

        SceneManager sceneManager = GlobalEnvironment.Instance.Get<SceneManager>();
        sceneManager.Enter(GameDefine.Scene.Login);
    }
}
