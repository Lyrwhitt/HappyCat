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

    private void Awake()
    {
        /*
        //player = GetComponent<Player>();

        // Player¶û ²¿ÀÓ
        skillDataManager = new DataManager<Dictionary<int, int>>(Path.Combine(Application.persistentDataPath, "SkillLevelData.json"));
        playerSkillModel = new PlayerSkillModel(player);

        inputAction = new PlayerInputAction();
        playerActions = inputAction.Player;

        SetSkillLevelData();
        */
    }

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
        for(int i = 0; i < playerSkillView.skillCells.Count; i++)
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

    private void OnBtnQStarted(InputAction.CallbackContext obj)
    {
        if (playerSkillView.btnQ.skillUsable)
        {
            DragDropSkillItem skill = playerSkillView.btnQ.GetSkill();

            if(skill != null)
            {
                playerSkillModel.UseSkill(skill);
                playerSkillView.btnQ.SetCooldown(skill.skillSO.attackData.cooldown);
            }
        }
    }

    private void OnBtnEStarted(InputAction.CallbackContext obj)
    {
        if (playerSkillView.btnE.skillUsable)
        {
            DragDropSkillItem skill = playerSkillView.btnE.GetSkill();

            if (skill != null)
            {
                playerSkillModel.UseSkill(skill);
                playerSkillView.btnE.SetCooldown(skill.skillSO.attackData.cooldown);
            }
        }
    }

    private void OnBtnRStarted(InputAction.CallbackContext obj)
    {
        if (playerSkillView.btnR.skillUsable)
        {
            DragDropSkillItem skill = playerSkillView.btnR.GetSkill();

            if (skill != null)
            {
                playerSkillModel.UseSkill(skill);
                playerSkillView.btnR.SetCooldown(skill.skillSO.attackData.cooldown);
            }
        }
    }

    private void OnBtnTStarted(InputAction.CallbackContext obj)
    {
        if (playerSkillView.btnT.skillUsable)
        {
            DragDropSkillItem skill = playerSkillView.btnT.GetSkill();

            if (skill != null)
            {
                playerSkillModel.UseSkill(skill);
                playerSkillView.btnT.SetCooldown(skill.skillSO.attackData.cooldown);
            }
        }
    }

    private void OnBtnFStarted(InputAction.CallbackContext obj)
    {
        if (playerSkillView.btnF.skillUsable)
        {
            DragDropSkillItem skill = playerSkillView.btnF.GetSkill();

            if (skill != null)
            {
                playerSkillModel.UseSkill(skill);
                playerSkillView.btnF.SetCooldown(skill.skillSO.attackData.cooldown);
            }
        }
    }
    private void OnBtnGStarted(InputAction.CallbackContext obj)
    {
        if (playerSkillView.btnG.skillUsable)
        {
            DragDropSkillItem skill = playerSkillView.btnG.GetSkill();

            if (skill != null)
            {
                playerSkillModel.UseSkill(skill);
                playerSkillView.btnG.SetCooldown(skill.skillSO.attackData.cooldown);
            }
        }
    }
}
