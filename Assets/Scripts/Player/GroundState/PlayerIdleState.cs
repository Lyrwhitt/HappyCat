using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        stateMachine.movementSpeedModifier = 0f;
        base.EnterState();

        SetAnimationBool(stateMachine.player.animationData.idleParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.idleParameterHash, false);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (stateMachine.movementInput != Vector2.zero)
        {
            OnMove();
            return;
        }
    }
}
