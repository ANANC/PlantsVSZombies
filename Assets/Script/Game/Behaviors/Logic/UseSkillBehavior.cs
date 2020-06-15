using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkillBehavior : LogicBehavior
{
    
    public class UseSkillBehaviorInfo : LogicBehaviorInfo
    {
        public MapObject mapObject;
        public Skill skill;
    }

    private UseSkillBehaviorInfo Info;
    public override void Enter()
    {
        Info = (UseSkillBehaviorInfo)Enviorment;
    }

    public override void Execute()
    {
        GlobalEnvironment.Instance.Get<SkillManager>().UseSkill(Info.mapObject, Info.skill);
    }
}
