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

    public override void Update()
    {
        GamePlay.Update();
    }

    public void InitBattleEnvironment()
    {
        GameMapObjectMgr.CreateShooter(Vector3.zero);
        GameMapObjectMgr.CreateShooter(Vector3.one);
        GameMapObjectMgr.CreateShooter(new Vector3(2, 1));
        GameMapObjectMgr.CreateShooter(new Vector3(2, 3));
        GameMapObjectMgr.CreateShooter(new Vector3(3, 0));
        GameMapObjectMgr.CreateShooter(new Vector3(4, 2));

    }


}
