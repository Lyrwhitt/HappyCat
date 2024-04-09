using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerSkillController : MonoBehaviour
{
    private DataManager<Dictionary<int, int>> skillDataManager;
    public Dictionary<int, int> skillLevelData = new Dictionary<int, int>();

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

    public void SetSkillController(Player player)
    {
        this.player = player;

        skillDataManager = new DataManager<Dictionary<int, int>>(Path.Combine(Application.persistentDataPath, "SkillLevelData.json"));
        playerSkillModel = new PlayerSkillModel(player);

        inputAction = new PlayerInputAction();
        playerActions = inputAction.Player;

        SetSkillLevelData();
    }

    private void SetSkillLevelData()
    {
        skillLevelData = skillDataManager.LoadData();

        if (skillLevelData == null)
            skillLevelData = playerSkillModel.InitPlayerSkillLevel();
        else
            playerSkillModel.InitPlayerSkillLevel(skillLevelData);
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
        playerSkillModel.UseSkill(playerSkillView.btnQ.GetSkill());
    }

    private void OnBtnEStarted(InputAction.CallbackContext obj)
    {
        playerSkillModel.UseSkill(playerSkillView.btnE.GetSkill());
    }

    private void OnBtnRStarted(InputAction.CallbackContext obj)
    {
        playerSkillModel.UseSkill(playerSkillView.btnR.GetSkill());
    }

    private void OnBtnTStarted(InputAction.CallbackContext obj)
    {
        playerSkillModel.UseSkill(playerSkillView.btnT.GetSkill());
    }

    private void OnBtnFStarted(InputAction.CallbackContext obj)
    {
        playerSkillModel.UseSkill(playerSkillView.btnF.GetSkill());
    }
    private void OnBtnGStarted(InputAction.CallbackContext obj)
    {
        playerSkillModel.UseSkill(playerSkillView.btnG.GetSkill());
    }
}
