using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GatlingPunchState : PlayerSkillState
{
    private bool alreadyApplyForce;
    private bool alreadyApplyCombo;

    private PlayerAttackData attackData;
    private AttackInfoData attackInfoData;

    private Skill skill;

    public GatlingPunchState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        attackData = Resources.Load<PlayerSkillSO>("Skills/GatlingPunch").attackData;
        //attackInfoData = attackData.AttackInfoDatas[0];
        skill = stateMachine.player.skillController.GetSkill(attackData.attackID);
    }

    public override void EnterState()
    {
        Debug.Log("뭐가문제지ㅅㅂ");

        base.EnterState();

        stateMachine.player.animationEventReceiver.animationEvent += DamageEnemy;

        SetAnimationBool(stateMachine.player.animationData.gatlingPunchParameterHash, true);

        alreadyApplyForce = false;
        alreadyApplyCombo = false;

        int comboIndex = stateMachine.comboIndex;

        attackInfoData = attackData.GetAttackInfo(comboIndex);
        //stateMachine.player.animator.SetInteger("GatlingPunchCombo", comboIndex);
    }

    public override void ExitState()
    {
        base.ExitState();

        stateMachine.player.animationEventReceiver.animationEvent -= DamageEnemy;

        SetAnimationBool(stateMachine.player.animationData.gatlingPunchParameterHash, false);

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
                if (collider.transform.TryGetComponent(out Health health))
                {
                    EffectManager.Instance.PlayEffect("PunchEffect", boxCenter);
                    health.TakeDamage(attackInfoData.damage * skill.GetSkillLevel());
                }

                if (collider.transform.TryGetComponent(out ForceReceiver forceReceiver))
                {
                    //rigidbody.velocity = Vector3.zero;
                    forceReceiver.AddForce(stateMachine.player.transform.forward * 3f + collider.transform.up * 12f);
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
        if (alreadyApplyForce) return;

        alreadyApplyForce = true;

        stateMachine.player.forceReceiver.ResetForceReceiver();

        // 전진공격
        stateMachine.player.forceReceiver.AddForce(stateMachine.player.transform.forward * attackInfoData.force);
        stateMachine.player.forceReceiver.drag = attackInfoData.drag;
    }

    public override void UpdateState()
    {
        //base.UpdateState();

        /*
        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.player.animator, "GatlingPunch");

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
                    stateMachine.ChangeState(stateMachine.gatlingPunchState);
                }
            }
        }
        // 모션 다보고 진행
        else
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }

        */
    }
}
