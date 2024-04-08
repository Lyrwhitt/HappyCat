using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCell : MonoBehaviour
{
    [HideInInspector] public DragDropSkillItem skill;
    private Transform cellImgTransform;

    private void Start()
    {
        cellImgTransform = this.transform.GetChild(0);
    }

    public DragDropSkillItem GetSkill()
    {
        return skill;
    }

    public void SetSkill(DragDropSkillItem skillItem)
    {
        skill = skillItem;
        skill.transform.SetParent(cellImgTransform);
    }

    public void RemoveSkill()
    {
        Destroy(skill.gameObject);
        skill = null;
    }
}
