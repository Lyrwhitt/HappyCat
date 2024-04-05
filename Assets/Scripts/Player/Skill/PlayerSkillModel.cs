using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillModel
{
    public Dictionary<int, ICommand> skillDictionary = new Dictionary<int, ICommand>();

    private Uppercut uppercut;

    public void InitPlayerSkill(Player player)
    {
        uppercut = new Uppercut(player);
        skillDictionary.Add(uppercut.attackID, uppercut);
    }

    public void UseSkill(DragDropSkillItem skill)
    {
        if (skill == null)
            return;
        
        skillDictionary[skill.skillSO.attackData.attackID].Execute();
    }
}
