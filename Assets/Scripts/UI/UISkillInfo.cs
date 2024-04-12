using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class UISkillInfo : MonoBehaviour
{
    [HideInInspector] public PlayerSkillSO skillSO;
    private int skillPoint = 0;

    private PlayerSkillController skillController;

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

    public void SetSkillData(PlayerSkillController controller, PlayerSkillSO playerSkillSO, int skillPoint)
    {
        this.skillController = controller;
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

        skillController.ChangeSkillLevel(skillSO.attackData.attackID, skillPoint);
    }

    private void OnPointDownBtnClicked()
    {
        skillPoint -= 1;
        skillPointText.SetText(skillPoint.ToString());

        skillController.ChangeSkillLevel(skillSO.attackData.attackID, skillPoint);
    }
}
