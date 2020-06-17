using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameScene : GameScene
{
    private Transform GardenParent;

    private GardenBattleGamePlay GamePlay;

    private GameMapObjectManager GameMapObjectMgr;

    public override void Init()
    {
        GamePlay = new GardenBattleGamePlay();
    }

    public override void UnInit()
    {

    }

    public override void Enter()
    {
        GameMapObjectMgr = GlobalEnvironment.Instance.Get<GameMapObjectManager>();
        GameMapObjectMgr.EnterBattle();

        GamePlay.Enter();

        InitBattleEnvironment();
    }

    public override void Exist()
    {
        GamePlay.Exist();
    }

    public override void Update()
    {
        GamePlay.Update();
    }

    public void InitBattleEnvironment()
    {
        GardenParent = new GameObject("Garden").transform;
        for (int h = 0; h < GameDefine.Garden.GardenHeight; h++)
        {
            for (int w = 0; w < GameDefine.Garden.GardenWidth; w++)
            {
                GameObject gameObject = GlobalEnvironment.Instance.Get<ResourceManager>().Instance(GameDefine.Path.Lawn);
                Transform transform = gameObject.transform;
                transform.position = new Vector3(w * GameDefine.Art.GardenCellSize.x, h * GameDefine.Art.GardenCellSize.y, 0);
                transform.SetParent(GardenParent);

                Color color = transform.GetComponent<MeshRenderer>().material.color;
                color.r += Random.Range(-0.08f, 0.08f);
                color.b += Random.Range(-0.08f, 0.08f);
                transform.GetComponent<MeshRenderer>().material.color = color;
            }
        }

        UIManager uiMgr = GlobalEnvironment.Instance.Get<UIManager>();
        MainUIController mainUIController = uiMgr.OpenUI<MainUIController>(GameDefine.UIName.MainUI);
        SelectPlantUIController selectPlantUIController = uiMgr.OpenSubUI<SelectPlantUIController>(GameDefine.UIName.MainUI, GameDefine.UIName.SelectPlantUI, mainUIController.TopLeftTransform);
        selectPlantUIController.RefreshCells(new GameDefine.PlantType[] { GameDefine.PlantType.Shooter });

        GameMapObjectMgr.CreateShooter(Vector3.zero);
        GameMapObjectMgr.CreateShooter(Vector3.one);
        GameMapObjectMgr.CreateShooter(new Vector3(2, 1));
        GameMapObjectMgr.CreateShooter(new Vector3(2, 3));
        GameMapObjectMgr.CreateShooter(new Vector3(3, 0));
        GameMapObjectMgr.CreateShooter(new Vector3(4, 2));

    }


}
