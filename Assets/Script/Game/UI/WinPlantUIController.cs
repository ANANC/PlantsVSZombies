using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPlantUIController : BaseUIObject
{
    private Button AgainButton;
    public override void Init()
    {
        AgainButton = FindComponent<Button>("Button");
        AgainButton.onClick.AddListener(AgainGame);
    }

    public void AgainGame()
    {
        GlobalEnvironment.Instance.Get<SceneManager>().Enter(GameDefine.Scene.Battle);
    }
}
