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


        Vector3 moveDirection = GetMovementDirection();

        if (moveDirection != Vector3.zero)
        {
            stateMachine.player.transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        stateMachine.player.forceReceiver.AddForce(stateMachine.player.transform.forward * playerGroundData.dashPower);


        stateMachine.player.forceReceiver.drag = playerGroundData.dashDrag;

        SetAnimationTrigger(stateMachine.player.animationData.dashParameterHash);
    }

    public override void ExitState()
    {
        base.ExitState();

        stateMachine.player.forceReceiver.ResetForceReceiver();
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

    protected override void AddInputActionsCallbacks()
    {
    }

    protected override void RemoveInputActionsCallbacks()
    {
    }
}
