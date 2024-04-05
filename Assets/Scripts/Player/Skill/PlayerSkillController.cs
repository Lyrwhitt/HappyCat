using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillController : MonoBehaviour
{
    private PlayerSkillModel playerSkillModel;
    public PlayerSkillView playerSkillView;

    [Header("Input")]
    public PlayerInputAction inputAction;
    public PlayerInputAction.PlayerActions playerActions;

    private Player player;

    /*
    private Dictionary<int, ICommand> skillDictionary = new Dictionary<int, ICommand>();

    public Uppercut uppercut;

    public ICommand qSkill;
    */

    private void Awake()
    {
        playerSkillModel = new PlayerSkillModel();

        player = GetComponent<Player>();

        inputAction = new PlayerInputAction();
        playerActions = inputAction.Player;

        //InitPlayerSkill();
        playerSkillModel.InitPlayerSkill(player);
    }

    private void Start()
    {
        AddInputActionsCallbacks();

        //qSkill = skillDictionary[1001];
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
    }

    private void OnBtnQStarted(InputAction.CallbackContext obj)
    {
        /*
        if (qSkill != null)
            qSkill.Execute();
        */
        playerSkillModel.UseSkill(playerSkillView.btnQ.GetSkill());
    }

    
    /*
    private void InitPlayerSkill()
    {
        uppercut = new Uppercut(this.player);
        skillDictionary.Add(uppercut.attackID, uppercut);
    }
    */
}
