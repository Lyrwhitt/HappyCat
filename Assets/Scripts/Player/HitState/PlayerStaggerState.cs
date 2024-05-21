using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStaggerState : PlayerHitState
{
    public PlayerStaggerState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        SetAnimationTrigger(stateMachine.player.animationData.staggerParameterHash);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (!damageReceiver.isStagger)
            stateMachine.ChangeState(stateMachine.idleState);
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
