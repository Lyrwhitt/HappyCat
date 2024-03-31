using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("System Input")]
    public SystemInputAction inputAction;
    public SystemInputAction.SystemActions systemActions;

    [Header("System Command")]
    private ICommand commandK;

    [Header("Game System")]
    private SkillMenuCommand skillMenuCommand;

    [Header("Skill")]
    public SkillMenu skillMenu;

    private void Start()
    {
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
    }

    private void AddInputActionsCallbacks()
    {
        systemActions.K.started += OnBtnKStarted;
    }

    private void OnBtnKStarted(InputAction.CallbackContext obj)
    {
        if (commandK != null)
            commandK.Execute();
    }
}
