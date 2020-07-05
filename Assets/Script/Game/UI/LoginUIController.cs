using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIController : BaseUIObject
{
    private Button BattleButton;
    public override void Init()
    {
        BattleButton = FindComponent<Button>("Button");
        BattleButton.onClick.AddListener(GotoBattle);
    }

    public void GotoBattle()
    {
        GlobalEnvironment.Instance.Get<SceneManager>().Enter(GameDefine.Scene.Battle);
    }
}
