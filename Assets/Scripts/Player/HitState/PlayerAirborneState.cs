using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAirborneState : PlayerHitState
{
    public PlayerAirborneState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        SetAnimationTrigger(stateMachine.player.animationData.airborneParameterHash);
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
