using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerBaseState
{
    public PlayerSkillState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void EnterState()
    {
        stateMachine.movementSpeedModifier = 0;
        stateMachine.player.forceReceiver.ResetForceReceiver();

        base.EnterState();

        SetAnimationBool(stateMachine.player.animationData.skillParameterHash, true);

        SetRotationForward();
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.skillParameterHash, false);

        stateMachine.player.forceReceiver.ResetForceReceiver();
    }
}
