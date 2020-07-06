using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginGameScene : GameScene
{
    AudioSource AudioSource;
    public override void Init()
    {
        GameObject Sound =  GlobalEnvironment.Instance.Get<ResourceManager>().Instance(GameDefine.Path.LoginSound);
        AudioSource = Sound.GetComponent<AudioSource>();
    }

    public override void UnInit()
    {
    }

    public override void Update()
    {
    }

    public override void Enter()
    {
        AudioSource.Play();
        GlobalEnvironment.Instance.Get<UIManager>().OpenUI<LoginUIController>(GameDefine.UIName.LoginPlant);
    }

    public override void Exist()
    {
        AudioSource.Stop();

        GlobalEnvironment.Instance.Get<UIManager>().DestroyUI(GameDefine.UIName.LoginPlant);

    }

}
