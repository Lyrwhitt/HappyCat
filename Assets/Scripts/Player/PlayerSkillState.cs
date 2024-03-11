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
        base.EnterState();

        SetAnimationBool(stateMachine.player.animationData.skillParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.skillParameterHash, false);
    }
}
