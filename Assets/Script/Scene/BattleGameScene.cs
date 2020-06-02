using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameScene : GameScene
{
    private GardenBattleGamePlay GamePlay;
    public override void Init()
    {
        GamePlay = new GardenBattleGamePlay();
    }

    public override void UnInit()
    {

    }

    public override void Enter()
    {
        GamePlay.Enter();

    }

    public override void Exist()
    {
        GamePlay.Exist();
    }


   
}
