using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillInfo : MonoBehaviour
{
    [HideInInspector] public PlayerSkillSO skillSO;

    public Image skillIcon;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillDescription;
    public TextMeshProUGUI skillPoint;

    public void SetSkillData(PlayerSkillSO playerSkillSO)
    {
        skillSO = playerSkillSO;

        SetSkillUI();
    }

    private void SetSkillUI()
    {
        skillIcon.sprite = skillSO.attackData.attackImg;
        skillName.SetText(skillSO.attackData.attackName);
        skillDescription.SetText(skillSO.attackData.attackDescription);
    }
}
