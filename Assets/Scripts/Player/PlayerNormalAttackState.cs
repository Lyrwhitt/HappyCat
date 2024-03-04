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

        // �޺��� ���� ���� ��������
        if (!alreadyApplyCombo)
            stateMachine.comboIndex = 0;
    }

    private void TryComboAttack()
    { 
        if (alreadyApplyCombo) return;
        // ������ ���ݱ��� ���� ��
        if (attackInfoData.comboStateIndex == -1) return;
        if (!stateMachine.isAttacking) return;

        alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if(alreadyApplyForce) return;

        alreadyApplyForce = true;

        stateMachine.player.forceReceiver.ResetForceReceiver();

        // ��������
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
            // ������ Ʈ������ Ÿ���� ���� �� ��, �޺� ����
            if (normalizedTime >= attackInfoData.forceTransitionTime)
                TryApplyForce();

            if (normalizedTime >= attackInfoData.comboTransitionTime)
            {
                TryComboAttack();

                if (alreadyApplyCombo)
                {
                    // �޺��� �����ϴ� �� (AttackData�� �� ������ ���� ������ �ε����� �������ִ�)
                    stateMachine.comboIndex = attackInfoData.comboStateIndex;
                    stateMachine.ChangeState(stateMachine.normalAttackState);
                }
            }
        }
        // ��� �ٺ��� ����
        else
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }
}
