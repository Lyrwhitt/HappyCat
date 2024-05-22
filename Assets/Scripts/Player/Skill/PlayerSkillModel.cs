using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerSkillModel
{
    private Player player;

    public Dictionary<int, Skill> skillDictionary = new Dictionary<int, Skill>();

    private Uppercut uppercut;
    private GatlingPunch gatlingPunch;

    public Dictionary<int, int> InitPlayerSkillLevel()
    {
        Dictionary<int, int> skillDatas = new Dictionary<int, int>();

        uppercut.SetSkillLevel(1);
        gatlingPunch.SetSkillLevel(1);

        skillDatas.Add(uppercut.skillId, 1);
        skillDatas.Add(gatlingPunch.skillId, 1);

        return skillDatas;
    }

    public void InitPlayerSkillLevel(Dictionary<int, int> skillDatas)
    {
        uppercut.SetSkillLevel(skillDatas[uppercut.skillId]);
        gatlingPunch.SetSkillLevel(skillDatas[gatlingPunch.skillId]);
    }

    public Skill GetPlayerSkill(int skillId)
    {
        return skillDictionary[skillId];
    }

    public void ChangePlayerSkillLevel(int skillId, int level)
    {
        skillDictionary[skillId].SetSkillLevel(level);
    }

    public float UseSkill(DragDropSkillItem skill)
    {
        if (skill == null)
        {
            Debug.Log("DragDropSkillItem is Null");
            return 0;
        }
        else
            return skillDictionary[skill.skillSO.attackData.attackID].Execute();
    }

    public PlayerSkillModel(Player player)
    {
        this.player = player;

        uppercut = new Uppercut(player);
        gatlingPunch = new GatlingPunch(player);

        skillDictionary.Add(uppercut.skillId, uppercut);
        skillDictionary.Add(gatlingPunch.skillId, gatlingPunch);
    }
}