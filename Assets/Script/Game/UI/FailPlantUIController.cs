using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailPlantUIController : BaseUIObject
{
    private Button LoginButton;
    public override void Init()
    {
        LoginButton = FindComponent<Button>("Button");
        LoginButton.onClick.AddListener(GotoLogin);
    }

    public void GotoLogin()
    {
        GlobalEnvironment.Instance.Get<SceneManager>().Enter(GameDefine.Scene.Login);
    }
}