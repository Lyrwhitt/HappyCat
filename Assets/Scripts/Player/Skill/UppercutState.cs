using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UppercutState : PlayerSkillState
{
    private bool alreadyApplyForce;

    private PlayerAttackData attackData;
    private AttackInfoData attackInfoData;

    private Skill skill;

    public UppercutState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        attackData = Resources.Load<PlayerSkillSO>("Skills/Uppercut").attackData;
        attackInfoData = attackData.AttackInfoDatas[0];
        //skillLevel = stateMachine.player.skillController.skillLevelData[attackData.attackID];
        skill = stateMachine.player.skillController.GetSkill(attackData.attackID);
    }

    public override void EnterState()
    {
        base.EnterState();

        alreadyApplyForce = false;

        stateMachine.player.animationEventReceiver.animationEvent += DamageEnemy;

        SetAnimationBool(stateMachine.player.animationData.uppercutParameterHash, true);
    }

    public override void ExitState()
    {
        base.ExitState();

        stateMachine.player.animationEventReceiver.animationEvent -= DamageEnemy;

        SetAnimationBool(stateMachine.player.animationData.uppercutParameterHash, false);
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
                if (collider.transform.TryGetComponent(out DamageReceiver damageReceiver))
                {
                    Vector3 force;
                    EffectManager.Instance.PlayEffect("PunchEffect", boxCenter);

                    if (!damageReceiver.isAirborne)
                    {
                        force = stateMachine.player.transform.forward * 3f + collider.transform.up * 12f;
                        damageReceiver.Damage(attackInfoData.damage * skill.GetSkillLevel(), force);
                        damageReceiver.Airborne();
                    }
                    else
                    {
                        force = stateMachine.player.transform.forward * 3f + collider.transform.up * 12f;
                        damageReceiver.Damage(attackInfoData.damage * skill.GetSkillLevel(), force);
                    }
                }
            }
        }
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

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.player.animator, "Uppercut");

        if (normalizedTime < 1f)
        {
            // 지정한 트랜지션 타임이 지난 후 힘, 콤보 적용
            if (normalizedTime >= attackInfoData.forceTransitionTime)
                TryApplyForce();
        }
        // 모션 다보고 진행
        else
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }
}
