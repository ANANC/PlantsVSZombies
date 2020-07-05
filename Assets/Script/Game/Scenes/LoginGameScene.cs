using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGameScene : GameScene
{

    public override void Init()
    {
    }

    public override void UnInit()
    {
    }

    public override void Update()
    {
    }

    public override void Enter()
    {
        GlobalEnvironment.Instance.Get<UIManager>().OpenUI<LoginUIController>(GameDefine.UIName.LoginPlant);
    }

    public override void Exist()
    {
        GlobalEnvironment.Instance.Get<UIManager>().DestroyUI(GameDefine.UIName.LoginPlant);

    }

}
