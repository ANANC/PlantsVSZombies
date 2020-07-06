using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : IManager
{

    private List<Skill> ExecuteSkillList;
    private List<Skill> AddSkillList;
    private List<Skill> DeleteSkillList;

    public void Init()
    {
        ExecuteSkillList = new List<Skill>();
        AddSkillList = new List<Skill>();
        DeleteSkillList = new List<Skill>();
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
                AddSkillList[index].Init();
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
            Skill skill = ExecuteSkillList[index];
            if (skill.Complete())
            {
                DeleteSkillList.Add(skill);
            }
        }
    }

    public void UseSkill(MapObject mapObject,Skill skill)
    {
        skill.mapObject = mapObject;
        AddSkillList.Add(skill);
    }


    public void ClearSkill(MapObject mapObject)
    {
        for(int index = 0;index< AddSkillList.Count;index++)
        {
            if(AddSkillList[index].mapObject == mapObject)
            {
                DeleteSkillList.Add(AddSkillList[index]);
            }
        }
        for (int index = 0; index < ExecuteSkillList.Count; index++)
        {
            if (ExecuteSkillList[index].mapObject == mapObject)
            {
                DeleteSkillList.Add(ExecuteSkillList[index]);
            }
        }
    }

}
