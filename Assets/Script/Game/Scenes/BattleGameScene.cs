using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameScene : GameScene
{
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

    public void InitBattleEnvironment()
    {
        GameMapObjectMgr.CreateShooter(Vector3.zero);

        GameMapObjectMgr.CreateZombieToMap(GameDefine.Path.Zombie, new Vector3(CellMap.GardenWidth+0.2f, 0, 0));
       
    }

  

}
