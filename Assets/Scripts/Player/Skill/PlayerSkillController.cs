using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerSkillController : MonoBehaviour
{
    private DataManager<Dictionary<int, int>> skillDataManager;
    private Dictionary<int, int> skillLevelData;

    private DataManager<Dictionary<int, string>> shortcutDataManager;
    private Dictionary<int, string> shortcutData;

    private PlayerSkillModel playerSkillModel;
    public PlayerSkillView playerSkillView;

    [Header("Input")]
    public PlayerInputAction inputAction;
    public PlayerInputAction.PlayerActions playerActions;

    private Player player;

    private void Start()
    {
        AddInputActionsCallbacks();
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    private void OnApplicationQuit()
    {
        SaveSkillLevelData();
        SaveShortcutData();
    }

    private void SaveSkillLevelData()
    {
        for (int i = 0; i < player.skillDatas.Length; i++)
        {
            PlayerSkillSO skillSO = player.skillDatas[i];

            skillLevelData[skillSO.attackData.attackID] =
                playerSkillModel.GetPlayerSkill(skillSO.attackData.attackID).GetSkillLevel();
        }

        skillDataManager.SaveData(skillLevelData);
    }

    private void SaveShortcutData()
    {
        shortcutData.Clear();

        for (int i = 0; i < playerSkillView.skillCells.Count; i++)
        {
            DragDropSkillItem skill = playerSkillView.skillCells[i].GetSkill();

            if (skill != null) 
            {
                shortcutData[i] = skill.skillSO.name;
            }
        }

        shortcutDataManager.SaveData(shortcutData);
    }

    public void SetSkillController(Player player)
    {
        this.player = player;

        skillDataManager = new DataManager<Dictionary<int, int>>(Path.Combine(Application.persistentDataPath, "SkillLevelData.json"));
        playerSkillModel = new PlayerSkillModel(player);

        shortcutDataManager = new DataManager<Dictionary<int, string>>(Path.Combine(Application.persistentDataPath, "ShortcutData.json"));



        inputAction = new PlayerInputAction();
        playerActions = inputAction.Player;

        SetSkillLevelData();
        SetShortcutData();
    }

    public void ChangeSkillLevel(int skillId, int skillLevel)
    {
        playerSkillModel.ChangePlayerSkillLevel(skillId, skillLevel);
    }

    public Skill GetSkill(int skillId) 
    {
        return playerSkillModel.GetPlayerSkill(skillId);
    }

    private void SetSkillLevelData()
    {
        skillLevelData = skillDataManager.LoadData();

        if(skillLevelData != null)
        {
            playerSkillModel.InitPlayerSkillLevel(skillLevelData);
        }
        else
        {
            skillLevelData = playerSkillModel.InitPlayerSkillLevel();
        }
    }

    private void SetShortcutData()
    {
        shortcutData = shortcutDataManager.LoadData();

        if(shortcutData != null)
        {
            foreach(KeyValuePair<int, string> data in shortcutData)
            {
                DragDropSkillItem skill = Instantiate(Resources.Load<DragDropSkillItem>("UI/SkillItem"));
                skill.SetSkill(Resources.Load<PlayerSkillSO>(string.Concat("Skills/", data.Value)));
                playerSkillView.skillCells[data.Key].SetSkill(skill);
            }
        }
        else
        {
            shortcutData = new Dictionary<int, string>();
        }
            
    }

    private void AddInputActionsCallbacks()
    {
        playerActions.Skill_Btn_Q.started += OnBtnQStarted;
        playerActions.Skill_Btn_E.started += OnBtnEStarted;
        playerActions.Skill_Btn_R.started += OnBtnRStarted;
        playerActions.Skill_Btn_T.started += OnBtnTStarted;
        playerActions.Skill_Btn_F.started += OnBtnFStarted;
        playerActions.Skill_Btn_G.started += OnBtnGStarted;
    }

    private void UseSkill(SkillCell btn)
    {
        if (btn.skillUsable)
        {
            DragDropSkillItem skill = btn.GetSkill();

            if (skill != null)
            {
                float cooldown = 0f;
                cooldown = playerSkillModel.UseSkill(skill);

                if (cooldown > 0f)
                {
                    btn.SetCooldown(cooldown);
                }
            }
        }
    }

    private void OnBtnQStarted(InputAction.CallbackContext obj)
    {
        UseSkill(playerSkillView.btnQ);
    }

    private void OnBtnEStarted(InputAction.CallbackContext obj)
    {
        UseSkill(playerSkillView.btnE);
    }

    private void OnBtnRStarted(InputAction.CallbackContext obj)
    {
        UseSkill(playerSkillView.btnR);
    }

    private void OnBtnTStarted(InputAction.CallbackContext obj)
    {
        UseSkill(playerSkillView.btnT);
    }

    private void OnBtnFStarted(InputAction.CallbackContext obj)
    {
        UseSkill(playerSkillView.btnF);
    }
    private void OnBtnGStarted(InputAction.CallbackContext obj)
    {
        UseSkill(playerSkillView.btnG);
    }
}
