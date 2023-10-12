using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        SetAnimationBool(stateMachine.player.animationData.airParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();
        SetAnimationBool(stateMachine.player.animationData.airParameterHash, false);
    }
}
