using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkillInfo : MonoBehaviour
{
    [HideInInspector] public PlayerSkillSO skillSO;
    private int skillPoint = 0;

    public Image skillIcon;
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillDescriptionText;
    public TextMeshProUGUI skillPointText;

    public Button pointUpBtn;
    public Button pointDownBtn;

    private void Awake()
    {
        pointUpBtn.onClick.AddListener(OnPointUpBtnClicked);
        pointDownBtn.onClick.AddListener(OnPointDownBtnClicked);
    }

    public void SetSkillData(PlayerSkillSO playerSkillSO, int skillPoint)
    {
        skillSO = playerSkillSO;
        this.skillPoint = skillPoint;

        SetSkillUI();
    }

    private void SetSkillUI()
    {
        skillIcon.sprite = skillSO.attackData.attackImg;
        skillNameText.SetText(skillSO.attackData.attackName);
        skillDescriptionText.SetText(skillSO.attackData.attackDescription);
        skillPointText.SetText(skillPoint.ToString());
    }

    private void OnPointUpBtnClicked()
    {
        skillPoint += 1;
        skillPointText.SetText(skillPoint.ToString());
    }

    private void OnPointDownBtnClicked()
    {
        skillPoint -= 1;
        skillPointText.SetText(skillPoint.ToString());
    }
}
