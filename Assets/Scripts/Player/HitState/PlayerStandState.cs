using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStandState : PlayerHitState
{
    public PlayerStandState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Enter Stand");

        SetAnimationTrigger(stateMachine.player.animationData.standParameterHash);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        float normalizedTime = GetNormalizedTime(stateMachine.player.animator, "Stand");

        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    public override void HandleInput()
    {
    }

    protected override void OnDashStarted(InputAction.CallbackContext context)
    {
    }
}
