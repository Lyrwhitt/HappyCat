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
        stateMachine.player.animationEventReceiver.SetAnimationEvent(attackData.attackID);

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
        Vector3 boxCenter = attackInfoData.hitBoxCenterOffset + stateMachine.player.transform.position + stateMachine.player.transform.forward *
            attackInfoData.hitBox.z / 2f;
        Quaternion boxRotation = Quaternion.LookRotation(stateMachine.player.transform.forward);

        stateMachine.player.testGizmo = new Test(true, attackInfoData.hitBox, boxCenter, boxRotation);

        Collider[] colliders = Physics.OverlapBox(boxCenter, attackInfoData.hitBox / 2f, boxRotation);

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
                        force = stateMachine.player.transform.forward * 5f + collider.transform.up * 15f;
                        damageReceiver.Damage(attackInfoData.damage * skill.GetSkillLevel(), force);
                        damageReceiver.Airborne(0.3f, -4.8f, 1f);
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

        stateMachine.player.forceReceiver.AddForce(stateMachine.player.transform.forward * attackInfoData.force);
        stateMachine.player.forceReceiver.drag = attackInfoData.drag;
    }

    public override void UpdateState()
    {
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
            SetPostDelay(0.2f);
        }
    }
}
