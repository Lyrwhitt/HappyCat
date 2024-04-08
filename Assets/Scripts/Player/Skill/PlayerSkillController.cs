using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillController : MonoBehaviour
{
    //DataManager<SkillData> skillDataManager = new DataManager<SkillData>("skillData.json");

    private PlayerSkillModel playerSkillModel;
    public PlayerSkillView playerSkillView;

    [Header("Input")]
    public PlayerInputAction inputAction;
    public PlayerInputAction.PlayerActions playerActions;

    private Player player;

    private void Awake()
    {
        playerSkillModel = new PlayerSkillModel();

        player = GetComponent<Player>();

        inputAction = new PlayerInputAction();
        playerActions = inputAction.Player;

        playerSkillModel.InitPlayerSkill(player);
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
