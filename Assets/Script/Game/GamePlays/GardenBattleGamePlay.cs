using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenBattleGamePlay : GamePlay
{
    private float[] KeyPoints;
    private float RefreshInterval;
    private float EruptRangeMin;
    private float EruptRangeMax;
    private float ContinueTime;

    private float CurTime;
    private float CurIntervalTime;
    private int CurPoint;

    private GameMapObjectManager GameMapObjectMgr;

    private int ZombieCount;

    public override void Enter()
    {
        GameMapObjectMgr = GlobalEnvironment.Instance.Get<GameMapObjectManager>();

        KeyPoints = new float[]
        {
            5,10
        };
        RefreshInterval = 6 * GameDefine.FrameValue;
        ContinueTime = 15 * GameDefine.FrameValue;
        EruptRangeMin = 1;
        EruptRangeMax = 4;

        CurTime = 0;
        CurIntervalTime = 0;
        CurPoint = 0;

        ZombieCount = 0;
    }

    public override void Exist()
    {

    }

    public override void Update()
    {
        if (CurTime <= ContinueTime)
        {
            CurTime += Time.deltaTime;

            CurIntervalTime += Time.deltaTime;
            if (CurIntervalTime > RefreshInterval)
            {
                CurIntervalTime = 0;
                CreateZombieToMap();
            }

            if (CurPoint < KeyPoints.Length)
            {
                if (CurTime > KeyPoints[CurPoint] * GameDefine.FrameValue)
                {
                    float count = Random.Range(EruptRangeMin, EruptRangeMax);
                    for (int index = 0; index < count; index++)
                    {
                        CreateZombieToMap();
                    }
                    CurPoint += 1;
                }
            }
        }
    }

    private void CreateZombieToMap()
    {
        ZombieCount += 1;
        GameMapObjectMgr.CreateZombieToMap(GameDefine.Path.Zombie, new Vector3(GameDefine.Garden.GardenWidth + 0.2f,Random.Range(0, GameDefine.Garden.GardenHeight), 0));
    }

    public void CharacterDeath(int layer, Vector3 position)
    {
        if (layer != LayerMask.NameToLayer(GameDefine.Layer.Zombie))
        {
            return;
        }

        ZombieCount -= 1;
        if (position.x > 0)
        {
            if(ZombieCount == 0)
            {
                GameMapObjectMgr.ExistBattle();
                GlobalEnvironment.Instance.Get<UIManager>().OpenUI<WinPlantUIController>(GameDefine.UIName.WinPlant);
            }
            return;
        }

        GameMapObjectMgr.ExistBattle();
        GlobalEnvironment.Instance.Get<UIManager>().OpenUI<FailPlantUIController>(GameDefine.UIName.FailPlant);
    }
}
