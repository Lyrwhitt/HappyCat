using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerGroundState
{
    private PlayerGroundData playerGroundData;

    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        playerGroundData = stateMachine.player.data.groundedData;

        stateMachine.player.forceReceiver.ResetForceReceiver();
        stateMachine.player.forceReceiver.AddForce(stateMachine.player.transform.forward * playerGroundData.dashPower);
        stateMachine.player.forceReceiver.drag = playerGroundData.dashDrag;

        SetAnimationBool(stateMachine.player.animationData.dashParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.dashParameterHash, false);
    }

    public override void UpdateState()
    {
        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.player.animator, "Dash");

        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
        
    }
}
