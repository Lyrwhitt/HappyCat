using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputAction inputAction;
    public PlayerInputAction.PlayerActions playerActions;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
        playerActions = inputAction.Player;
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }
}
