using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCell : MonoBehaviour
{
    [HideInInspector] public DragDropSkillItem skill;

    public DragDropSkillItem GetSkill()
    {
        return skill;
    }

    public void SetSkill(DragDropSkillItem skillItem)
    {
        skill = skillItem;
        skillItem.canvasGroup.blocksRaycasts = true;
        skill.transform.SetParent(transform.GetChild(0), false);
    }

    public void RemoveSkill()
    {
        Destroy(skill.gameObject);
        skill = null;
    }
}
