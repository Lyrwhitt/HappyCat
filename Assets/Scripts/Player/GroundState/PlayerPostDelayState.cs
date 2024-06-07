using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPostDelayState : PlayerBaseState
{
    public PlayerPostDelayState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        SetAnimationFloat(stateMachine.player.animationData.speedParameterHash, 0f);
        SetAnimationBool(stateMachine.player.animationData.groundParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.groundParameterHash, false);
    }


    public override void UpdateState()
    {
        if (!stateMachine.isPostDelay)
            return;

        stateMachine.postDelay -= Time.deltaTime;

        if (stateMachine.postDelay <= 0f)
        {
            stateMachine.postDelay = 0f;
            stateMachine.isPostDelay = false;

            stateMachine.ChangeState(stateMachine.idleState);
        }
    }
}

