using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        SetAnimationBool(stateMachine.player.animationData.groundParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.groundParameterHash, false);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (stateMachine.isAttacking)
        {
            OnAttack();
            return;
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (damageReceiver.isAirborne)
            return;

        if (!stateMachine.player.groundDetection.isGrounded
        && stateMachine.player.controller.velocity.y < stateMachine.player.forceReceiver.gravity * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.fallState);
            return;
        }

        float targetSpeed = 0f;

        if (stateMachine.movementInput != Vector2.zero)
        {
            targetSpeed = stateMachine.movementSpeed;
        }

        stateMachine.animationBlend = Mathf.Lerp(stateMachine.animationBlend, targetSpeed, Time.fixedDeltaTime * groundData.speedChangeRate);

        if (stateMachine.animationBlend < 0.01f)
            stateMachine.animationBlend = 0f;
        

        SetAnimationFloat(stateMachine.player.animationData.speedParameterHash, stateMachine.animationBlend);
    }

    /*
    protected override void OnBtnQStarted(InputAction.CallbackContext obj)
    {
        stateMachine.player.skillController.qSkill.Execute();
    }
    */

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        if (damageReceiver.isStagger || damageReceiver.isAirborne)
            return;

        stateMachine.ChangeState(stateMachine.jumpState);
    }

    // 아마 이쪽에서 대쉬끊기는 문제 발생하는듯
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if(stateMachine.movementInput == Vector2.zero)
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.idleState);

        base.OnMovementCanceled(context);
    }

    protected virtual void OnMove()
    {
        if (damageReceiver.isStagger || damageReceiver.isAirborne)
            return;

        stateMachine.ChangeState(stateMachine.runState);
    }

    protected virtual void OnAttack()
    {
        if (damageReceiver.isStagger || damageReceiver.isAirborne)
            return;

        stateMachine.ChangeState(stateMachine.normalAttackState);
    }
}
