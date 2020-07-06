using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    private const float Frame = 30;
    private float DeltaTime;
    private float CurDeltaTime;

    private void Awake()
    {
        Screen.SetResolution(1024, 768, false);
    }

    void Start()
    {
        DeltaTime = 1 / Frame;
        CurDeltaTime = 0;

        FormworkRegister();
        GameRegister();

        GlobalEnvironment.Instance.Start();

        StartGame();
    }

    void Update()
    {
        CurDeltaTime += Time.deltaTime;
        if(CurDeltaTime< DeltaTime)
        {
            return;
        }
        CurDeltaTime = 0;
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
