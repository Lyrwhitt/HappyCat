using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        SetAnimationBool(stateMachine.player.animationData.hitParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.hitParameterHash, false);
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
