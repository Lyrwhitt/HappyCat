using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void EnterState()
    {
        stateMachine.jumpForce = stateMachine.player.data.airData.jumpForce;
        stateMachine.player.forceReceiver.Jump(stateMachine.jumpForce);

        base.EnterState();

        SetAnimationBool(stateMachine.player.animationData.jumpParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.jumpParameterHash, false);
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if(stateMachine.player.controller.velocity.y <= 0f)
        {
            stateMachine.ChangeState(stateMachine.fallState);
            return;
        }
    }
}
