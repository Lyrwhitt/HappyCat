using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        SetAnimationBool(stateMachine.player.animationData.fallParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.fallParameterHash, false);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (stateMachine.player.groundDetection.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.idleState);
            return;
        }
    }
}
