using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        inputAction = new SystemInputAction();
        systemActions = inputAction.System;

        SetGameSystem();
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }



    public Player player;

    [Header("Manager")]
    public ItemCreator itemCreator;

    [Header("System Input")]
    public SystemInputAction inputAction;
    public SystemInputAction.SystemActions systemActions;

    [Header("System Command")]
    private ICommand commandK;
    private ICommand commandI;

    [Header("Game System")]
    private SkillMenuCommand skillMenuCommand;
    private InventoryCommand inventoryCommand;

    [Header("Skill")]
    public SkillMenu skillMenu;

    [Header("Inventory")]
    public InventoryView inventory;

    private void Start()
    {
        itemCreator = this.GetComponent<ItemCreator>();

        InitSystemCommand();

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

    private void SetGameSystem()
    {
        skillMenuCommand = new SkillMenuCommand(skillMenu);
        inventoryCommand = new InventoryCommand(inventory);
    }

    public void ChangeCursorLockMode(CursorLockMode cursorLockMode)
    {
        switch (cursorLockMode)
        {
            case CursorLockMode.Locked:
                Cursor.lockState = CursorLockMode.Locked;
                player.playerCameraBrain.enabled = true;
                player.input.enabled = true;

                break;

            case CursorLockMode.None:
                Cursor.lockState = CursorLockMode.None;
                player.playerCameraBrain.enabled = false;
                player.input.enabled = false;

                break;
        }
    }

    private void InitSystemCommand()
    {
        commandK = skillMenuCommand;
        commandI = inventoryCommand;
    }

    private void AddInputActionsCallbacks()
    {
        systemActions.K.started += OnBtnKStarted;
        systemActions.I.started += OnBtnIStarted;
    }

    private void OnBtnKStarted(InputAction.CallbackContext obj)
    {
        if (commandK != null)
            commandK.Execute();
    }

    private void OnBtnIStarted(InputAction.CallbackContext obj)
    {
        if (commandI != null)
            commandI.Execute();
    }
}
