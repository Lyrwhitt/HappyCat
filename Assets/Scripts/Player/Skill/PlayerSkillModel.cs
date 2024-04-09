using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillModel
{
    private Player player;

    public Dictionary<int, ICommand> skillDictionary = new Dictionary<int, ICommand>();

    private Uppercut uppercut;

    public Dictionary<int, int> InitPlayerSkillLevel()
    {
        Dictionary<int, int> skillDatas = new Dictionary<int, int>();

        uppercut.SetSkillLevel(1);

        skillDatas.Add(uppercut.attackID, 1);

        return skillDatas;
    }

    public void InitPlayerSkillLevel(Dictionary<int, int> skillDatas)
    {
        uppercut.SetSkillLevel(skillDatas[uppercut.attackID]);
    }

    public void UseSkill(DragDropSkillItem skill)
    {
        if (skill == null)
            return;
        
        skillDictionary[skill.skillSO.attackData.attackID].Execute();
    }

    public PlayerSkillModel(Player player)
    {
        this.player = player;

        uppercut = new Uppercut(player);

        skillDictionary.Add(uppercut.attackID, uppercut);
    }
}