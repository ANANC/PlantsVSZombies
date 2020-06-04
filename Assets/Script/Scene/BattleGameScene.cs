using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameScene : GameScene
{
    private GardenBattleGamePlay GamePlay;
    private SkillManager skillMgr;

    public override void Init()
    {
        GamePlay = new GardenBattleGamePlay();
    }

    public override void UnInit()
    {

    }

    public override void Enter()
    {
        skillMgr = GlobalEnvironment.Instance.Get<SkillManager>();

        GamePlay.Enter();

        MapObject shooter = GamePlay.CreatePlantToMap(GameDefine.Path.Plant, Vector3.zero);
        skillMgr.UseSkill(new FireBulletSkill(), shooter);
    }

    public override void Exist()
    {
        GamePlay.Exist();
    }


   
}
