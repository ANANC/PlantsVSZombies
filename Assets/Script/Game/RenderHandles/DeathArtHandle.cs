using UnityEngine;
using UnityEditor;

public class DeathArtHandle : RepresentHandle
{
    public override RepresentManager.ExecuteOrder Order()
    {
        return RepresentManager.ExecuteOrder.Final;
    }


    public override void Execute(MapObject mapObject)
    {
        MapObjectAttribute attribute = mapObject.GetAttribute<MapObjectAttribute>();
        MapObjectArtAttribute art = mapObject.GetAttribute<MapObjectArtAttribute>();

        if (attribute == null || art == null)
        {
            return;
        }

        if (attribute.Hp > 0)
        {
            return;
        }

        int layer = art.gameObject.layer;
        Vector3 position = art.transform.position;

        GlobalEnvironment.Instance.Get<GameMapObjectManager>().DestroyMapObject(mapObject);

        BattleGameScene battleGameScene = (BattleGameScene)GlobalEnvironment.Instance.Get<SceneManager>().GetScene(GameDefine.Scene.Battle);
        GardenBattleGamePlay gardenBattleGamePlay = (GardenBattleGamePlay)battleGameScene.GamePlay;
        gardenBattleGamePlay.CharacterDeath(layer, position);
    }
}