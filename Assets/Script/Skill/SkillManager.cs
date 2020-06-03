using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : IManager
{

    private List<BehaviorTree> ExecuteSkillList;
    private List<BehaviorTree> AddSkillList;
    private List<BehaviorTree> DeleteSkillList;

    public void Init()
    {
        ExecuteSkillList = new List<BehaviorTree>();
        AddSkillList = new List<BehaviorTree>();
        DeleteSkillList = new List<BehaviorTree>();
    }

    public void UnInit()
    {
    }

    public void Start()
    {
    }



    public void Update()
    {
        if (AddSkillList.Count > 0)
        {
            for (int index = 0; index < AddSkillList.Count; index++)
            {
                AddSkillList[index].Execute();
                ExecuteSkillList.Add(AddSkillList[index]);
            }
            AddSkillList.Clear();
        }

        if (DeleteSkillList.Count > 0)
        {
            for (int index = 0; index < DeleteSkillList.Count; index++)
            {
                ExecuteSkillList.Remove(DeleteSkillList[index]);
            }
            DeleteSkillList.Clear();
        }

        for (int index = 0; index < ExecuteSkillList.Count; index++)
        {
            BehaviorTree behaviorTree = ExecuteSkillList[index];
            if (behaviorTree.Complete())
            {
                DeleteSkillList.Add(behaviorTree);
            }
        }
    }

    public void AddSkill(BehaviorTree skill)
    {
        AddSkillList.Add(skill);
    }

    // ----------------------------------------------------------------------------
    public void FireBullet(MapObject user)
    {
        BehaviorTree behaviorTree = new BehaviorTree();

        CreateMapObjectBehavior createMapObjectBehavior = new CreateMapObjectBehavior();
        CreateMapObjectBehavior.CreateBehaviorInfo createBehaviorInfo = new CreateMapObjectBehavior.CreateBehaviorInfo();
        createBehaviorInfo.Position = user.GetAttribute<MapOjectAttribute>().Position + Vector3.right * 20;
        createBehaviorInfo.ResourcePath = GameDefine.Path.Bullet;
        createMapObjectBehavior.Enviorment = createBehaviorInfo;
        SingleBehavior singleBehavior = new SingleBehavior();
        singleBehavior.LogicBehavior = createMapObjectBehavior;
        createMapObjectBehavior.Node = singleBehavior;
        behaviorTree.AddBehavior<SingleBehavior>(singleBehavior, BehaviorTree.NodeType.Serial);

        AddSkill(behaviorTree);
    }

}
