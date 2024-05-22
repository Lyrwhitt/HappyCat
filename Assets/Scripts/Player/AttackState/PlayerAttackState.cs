using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void EnterState()
    {
        stateMachine.movementSpeedModifier = 0;
        base.EnterState();

        SetAnimationBool(stateMachine.player.animationData.attackParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.attackParameterHash, false);

        stateMachine.player.forceReceiver.ResetForceReceiver();
    }
}
