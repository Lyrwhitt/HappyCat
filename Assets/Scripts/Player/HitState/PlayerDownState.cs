using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDownState : PlayerHitState
{
    public PlayerDownState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Enter Down");

        SetAnimationTrigger(stateMachine.player.animationData.downParameterHash);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    public override void HandleInput()
    {
    }

    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
    }
}
