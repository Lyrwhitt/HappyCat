using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
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

        stateMachine.player.animationEventReceiver.animationEvent += DamageEnemy;
        stateMachine.player.animationEventReceiver.SetAnimationEvent(stateMachine.player.data.attackData.attackID);

        SetAnimationBool(stateMachine.player.animationData.comboAttackParameterHash, true);

        alreadyApplyForce = false;
        alreadyApplyCombo = false;

        int comboIndex = stateMachine.comboIndex;
        attackInfoData = stateMachine.player.data.attackData.GetAttackInfo(comboIndex);
        stateMachine.player.animator.SetInteger("Combo", comboIndex);

        if(comboIndex == 0)
            SetRotationForward();
    }

    public override void ExitState()
    {
        base.ExitState();

        stateMachine.player.animationEventReceiver.animationEvent -= DamageEnemy;

        SetAnimationBool(stateMachine.player.animationData.comboAttackParameterHash, false);

        // 콤보에 성공 하지 못했을떄
        if (!alreadyApplyCombo)
            stateMachine.comboIndex = 0;
    }

    public void DamageEnemy()
    {
        // 플레이어의 위치와 방향을 기준으로 사각형 모양의 영역을 생성합니다.
        Vector3 boxCenter = attackInfoData.hitBoxCenterOffset + stateMachine.player.transform.position + stateMachine.player.transform.forward *
            attackInfoData.hitBox.z / 2f;
        Quaternion boxRotation = Quaternion.LookRotation(stateMachine.player.transform.forward);

        stateMachine.player.testGizmo = new Test(true, attackInfoData.hitBox, boxCenter, boxRotation);

        // OverlapBox 함수를 통해 사각형 모양의 영역 안에 있는 적을 검출합니다.
        Collider[] colliders = Physics.OverlapBox(boxCenter, attackInfoData.hitBox / 2f, boxRotation);

        // 각각의 충돌한 오브젝트에 대해 처리합니다.
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if(collider.transform.TryGetComponent(out DamageReceiver damageReceiver))
                {
                    Vector3 force;
                    EffectManager.Instance.PlayEffect("PunchEffect", boxCenter);

                    if (!damageReceiver.isAirborne)
                    {
                        force = stateMachine.player.transform.forward;
                        damageReceiver.Damage(attackInfoData.damage, force);
                        damageReceiver.Stagger(0.5f);
                    }
                    else
                    {
                        force = stateMachine.player.transform.forward * 3f + collider.transform.up * 12f;
                        damageReceiver.Damage(attackInfoData.damage, force);
                    }
                }
            }
        }
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
