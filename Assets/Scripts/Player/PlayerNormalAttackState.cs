using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackState : PlayerAttackState
{
    private bool alreadyApplyForce;
    private bool alreadyApplyCombo;

    AttackInfoData attackInfoData;

    public PlayerNormalAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        SetAnimationBool(stateMachine.player.animationData.comboAttackParameterHash, true);

        alreadyApplyForce = false;
        alreadyApplyCombo = false;

        int comboIndex = stateMachine.comboIndex;
        attackInfoData = stateMachine.player.data.attackData.GetAttackInfo(comboIndex);
        stateMachine.player.animator.SetInteger("Combo", comboIndex);
    }

    public override void ExitState()
    {
        base.ExitState();

        SetAnimationBool(stateMachine.player.animationData.comboAttackParameterHash, false);

        // 콤보에 성공 하지 못했을떄
        if (!alreadyApplyCombo)
            stateMachine.comboIndex = 0;
    }

    private void TryComboAttack()
    { 
        if (alreadyApplyCombo) return;
        // 마지막 공격까지 했을 때
        if (attackInfoData.comboStateIndex == -1) return;
        if (!stateMachine.isAttacking) return;

        alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if(alreadyApplyForce) return;

        alreadyApplyForce = true;

        stateMachine.player.forceReceiver.ResetForceReceiver();

        // 전진공격
        stateMachine.player.forceReceiver.AddForce(stateMachine.player.transform.forward * attackInfoData.force);
        stateMachine.player.forceReceiver.drag = attackInfoData.drag;
    }

    public override void UpdateState()
    {
        //base.UpdateState();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.player.animator, "Attack");

        if (normalizedTime < 1f)
        {
            // 지정한 트랜지션 타임이 지난 후 힘, 콤보 적용
            if (normalizedTime >= attackInfoData.forceTransitionTime)
                TryApplyForce();

            if (normalizedTime >= attackInfoData.comboTransitionTime)
            {
                TryComboAttack();

                if (alreadyApplyCombo)
                {
                    // 콤보가 증가하는 곳 (AttackData의 각 공격은 다음 공격의 인덱스를 가지고있다)
                    stateMachine.comboIndex = attackInfoData.comboStateIndex;
                    stateMachine.ChangeState(stateMachine.normalAttackState);
                }
            }
        }
        // 모션 다보고 진행
        else
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }
}
